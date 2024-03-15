// src/components/Chat.tsx
import React, { useEffect, useState } from "react";
import MessageList from "./MessageList";
import MessageForm from "./MessageForm";
import IMessage from "../../models/chats/IMessage";
import { SignalRProvider, useSignalR } from "../SignalRContext";
import * as signalR from "@microsoft/signalr";

const Chat: React.FC = () => {
  // const { connection, sendMessage } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);

  // useEffect(() => {
  //   if (connection) {
  //     connection.on("Receive", (content: string) => {
  //       const newMessage = {
  //         id: Date.now(),
  //         author: "User",
  //         content,
  //         avatar: "https://example.com/avatar.jpg",
  //       };
  //       setMessages((prevMessages) => [...prevMessages, newMessage]);
  //     });
  //   }

  //   return () => {
  //     if (connection) {
  //       connection.off("Receive");
  //     }
  //   };
  // }, [connection]);

  const newConnection = new signalR.HubConnectionBuilder()
    // http://localhost:10000/
    .withUrl(import.meta.env.VITE_SIGNALR_URL + "chat", {
      // Todo: ?
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets,
    })
    .build();

  newConnection.on("Receive", function (message: string) {
    console.log("Received message: ", message);
    // const newMessage: IMessage = {
    //   id: Date.now(),
    //   author: "User",
    //   content: message,
    //   avatar: "https://example.com/avatar.jpg",
    // };
    const newMessage = { content: message };
    setMessages((prevMessages) => [...prevMessages, newMessage]);
  });

  newConnection
    .start()
    .then(() => {
      console.log("Started");
    })
    .catch((err) => console.error(err.toString()));

  console.log("Connection state: ", newConnection.state);

  const handleSend = async (content: string) => {
    const newMessage = {
      id: Date.now(),
      author: "User",
      content,
      avatar: "https://example.com/avatar.jpg",
    };
    console.log("Handle send!");
    newConnection
      .send("SendMessage", content)
      .catch((err) => console.error(err.toString()));

    // await sendMessage(newMessage.content);
  };

  // const onReceive = (message: string) {
  //   console.log("Received message!", message);
  // }

  return (
    <SignalRProvider>
      <>
        <MessageList messages={messages} />
        <MessageForm onSend={handleSend} />
      </>
    </SignalRProvider>
  );
};

export default Chat;
