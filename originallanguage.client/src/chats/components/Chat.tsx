// src/components/Chat.tsx
import React, { useEffect, useState } from "react";
import MessageList from "./MessageList";
import MessageForm from "./MessageForm";
import IMessage from "../models/IMessage";
import { useSignalR } from "../SignalRContext";

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

  // const newConnection = new signalR.HubConnectionBuilder()
  //   // http://localhost:10000/
  //   .withUrl(import.meta.env.VITE_SIGNALR_URL + "chat", {
  //     // Todo: ?
  //     skipNegotiation: true,
  //     transport: signalR.HttpTransportType.WebSockets,
  //   })
  //   .build();

  // newConnection.on("ReceiveMessage", function (message: any) {
  //   console.log("Received message: ", message);
  // const newMessage: IMessage = {
  //   id: Date.now(),
  //   author: "User",
  //   content: message,
  //   avatar: "https://example.com/avatar.jpg",
  // };
  // const newMessage = { content: message };
  // setMessages((prevMessages) => [...prevMessages, newMessage]);
  // });

  // newConnection
  //   .start()
  //   .then(() => {
  //     console.log("Started");
  //   })
  //   .catch((err) => console.error(err.toString()));

  // console.log("Connection state: ", newConnection.state);

  const handleSend = async (content: string) => {
    const newMessage = {
      id: Date.now(),
      author: "User",
      content,
      avatar: "https://example.com/avatar.jpg",
    };
    console.log("Handle send!");
    // newConnection
    //   .send("SendMessage", newMessage)
    //   .catch((err) => console.error(err.toString()));

    await sendMessage(newMessage);
  };

  // const onReceive = (message: string) {
  //   console.log("Received message!", message);
  // }

  return (
    <>
      <MessageList messages={messages} />
      <MessageForm onSend={handleSend} />
    </>
  );
};

export default Chat;
