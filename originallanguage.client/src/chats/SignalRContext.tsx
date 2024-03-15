import React, {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import * as signalR from "@microsoft/signalr";
import IMessage from "./models/IMessage";

interface SignalRContextType {
  connection: signalR.HubConnection | null;
  sendMessage: (message: IMessage) => Promise<void>;
}

const SignalRContext = createContext<SignalRContextType>({
  connection: null,
  sendMessage: async () => {
    console.error("Using default SignalR context");
  },
});

export const useSignalR = () => useContext(SignalRContext);

interface SignalRProviderProps {
  children?: ReactNode;
}

export const SignalRProvider: React.FC<SignalRProviderProps> = ({
  children,
}: SignalRProviderProps) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(import.meta.env.VITE_SIGNALR_URL + "chat", {
        // Todo: ?
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    setConnection(newConnection);

    newConnection.on("ReceiveMessage", function (message: IMessage) {
      console.log("Received message: ", message);
    });

    newConnection.start().catch((err) => console.error(err.toString()));

    return () => {
      newConnection.stop();
    };
  }, []);

  const sendMessage = async (message: IMessage) => {
    console.log("Sending message...");

    if (!connection) {
      console.error("Connection is not established");
      return;
    }
    try {
      await connection.invoke("SendMessage", message);
    } catch (err: any) {
      console.error(err.toString());
    }
  };

  const contextValue = useMemo(
    () => ({
      connection,
      sendMessage,
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
