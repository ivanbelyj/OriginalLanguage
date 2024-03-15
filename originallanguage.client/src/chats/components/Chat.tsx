import React, { useEffect, useState } from "react";
import MessageList from "./MessageList";
import MessageForm from "./MessageForm";
import IMessage from "../models/IMessage";
import { useSignalR } from "../SignalRContext";
import "./styles/Chat.css";
import { useAuth } from "../../auth/AuthProvider";

const Chat: React.FC = () => {
  const { connection, sendMessage } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);
  const { getDecodedToken } = useAuth();
  const decodedToken = getDecodedToken();
  const userId = decodedToken?.sub;

  useEffect(() => {
    console.log("connection", connection);
    if (connection) {
      console.log("Connection state: ", connection.state);
      connection.on("ReceiveMessage", (message: IMessage) => {
        console.log("Receive message!", message);

        // Todo: handle it in another place ?
        console.log("message datetime", message.dateTime);
        message.dateTime = new Date(message.dateTime);

        setMessages((prevMessages) => [...prevMessages, message]);
      });
    }

    return () => {
      if (connection) {
        connection.off("ReceiveMessage");
      }
    };
  }, [connection]);

  const handleSend = async (content: string) => {
    const newMessage = {
      content,
      userId,
    };

    await sendMessage(newMessage);
  };

  return (
    <div className="chat">
      <MessageList messages={messages} />
      <MessageForm onSend={handleSend} />
    </div>
  );
};

export default Chat;
