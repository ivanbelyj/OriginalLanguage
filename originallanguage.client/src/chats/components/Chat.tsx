import React, { useEffect, useState } from "react";
import MessageList from "./MessageList";
import MessageForm from "./MessageForm";
import IMessage from "../models/IMessage";
import { useSignalR } from "../SignalRContext";
import "./Chat.css";

const Chat: React.FC = () => {
  const { connection, sendMessage } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);

  useEffect(() => {
    console.log("connection", connection);
    if (connection) {
      console.log("Connection state: ", connection.state);
      connection.on("ReceiveMessage", (message: IMessage) => {
        console.log("Receive message!", message);
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
      id: Date.now(),
      author: "User",
      content,
      avatar: "https://example.com/avatar.jpg",
    };
    console.log("Handle send!");

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
