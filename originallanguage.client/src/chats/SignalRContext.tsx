// src/contexts/SignalRContext.tsx
import React, {
  ReactNode,
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import * as signalR from "@microsoft/signalr";

interface SignalRContextType {
  connection: signalR.HubConnection | null;
  sendMessage: (message: string) => Promise<void>;
}

const SignalRContext = createContext<SignalRContextType>({
  connection: null,
  sendMessage: async () => {
    console.log("test");
  },
});

export const useSignalR = () => useContext(SignalRContext);

interface SignalRProviderProps {
  children?: ReactNode;
  //   onReceive: (message: string) => {};
}

export const SignalRProvider: React.FC<SignalRProviderProps> = ({
  children,
}: //   onReceive,
SignalRProviderProps) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      // http://localhost:10000/
      .withUrl(import.meta.env.VITE_SIGNALR_URL + "chat", {
        // Todo: ?
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    setConnection(newConnection);

    newConnection.on("Receive", function (message: string) {
      console.log("Received message: ", message);
    });

    newConnection.start().catch((err) => console.error(err.toString()));

    return () => {
      newConnection.stop();
    };
  }, []);

  //   const sendMessage = async (message: string) => {
  //     console.log("Sending message...");
  //     try {
  //       await connection?.invoke("SendMessage", message);
  //     } catch (err: any) {
  //       console.error(err.toString());
  //     }
  //   };

  const sendMessage = async (message: string) => {
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
    []
  );
  console.log("ctx", contextValue);
  return (
    <SignalRContext.Provider value={contextValue}>
      {children}
    </SignalRContext.Provider>
  );
};
