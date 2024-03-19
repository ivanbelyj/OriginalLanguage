import React, {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import * as signalR from "@microsoft/signalr";
import { useJwtToken } from "../auth/AuthProvider";

export interface ISendMessageModel {
  content: string;
}

interface SignalRContextType {
  connection: signalR.HubConnection | null;
  sendMessage: (groupId: string, message: ISendMessageModel) => Promise<void>;
  joinGroup: (groupId: string) => Promise<void>;
}

const printDefaultContextError = async () => {
  console.error("Using default SignalR context");
};

const SignalRContext = createContext<SignalRContextType>({
  connection: null,
  sendMessage: printDefaultContextError,
  joinGroup: printDefaultContextError,
});

export const useSignalR = () => useContext(SignalRContext);

interface SignalRProviderProps {
  children?: ReactNode;
}

export const SignalRProvider: React.FC<SignalRProviderProps> = ({
  children,
}: SignalRProviderProps) => {
  const { token } = useJwtToken();
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_SIGNALR_URL + "chat", {
        accessTokenFactory: () => {
          return token ?? "";
        },
        // Todo: ?
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.start().catch((err) => console.error(err.toString()));

    return () => {
      newConnection.stop();
    };
  }, []);

  const sendMessage = async (groupId: string, message: ISendMessageModel) => {
    console.log("Sending message...", message, "to group", groupId);

    if (!connection) {
      console.error("Connection is not established");
      return;
    }
    try {
      await connection.invoke("SendMessage", groupId, message);
    } catch (err: any) {
      console.error(err.toString());
    }
  };

  const joinGroup = async (groupId: string) => {
    if (!connection) {
      console.error("Connection is not established");
      return;
    }
    try {
      console.log("Joining group...");
      await connection.invoke("JoinGroup", groupId);
    } catch (err: any) {
      console.error(err.toString());
    }
  };

  const contextValue = useMemo(
    () => ({
      connection,
      sendMessage,
      joinGroup,
    }),
    [connection]
  );

  console.log("ctx", contextValue);
  return (
    <SignalRContext.Provider value={contextValue}>
      {children}
    </SignalRContext.Provider>
  );
};
