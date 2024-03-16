import React, { useEffect, useState } from "react";
import MessageList from "./MessageList";
import MessageForm from "./MessageForm";
import IMessage from "../models/IMessage";
import { useSignalR } from "../SignalRContext";
import "./styles/Chat.css";

interface ChatProps {
  groupId?: string;
}

const defaultGroupName = "main";

const Chat: React.FC<ChatProps> = ({ groupId: groupIdInitial }: ChatProps) => {
  const { connection, sendMessage, joinGroup } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);

  const groupId = groupIdInitial ?? defaultGroupName;

  useEffect(() => {
    console.log("connection", connection);
    if (connection) {
      console.log("Connection state: ", connection.state);

      joinGroup(groupId);

      connection.on("ReceiveMessage", (message: IMessage) => {
        console.log("Receive message!", message);
        if (message.groupId == groupId) {
          // Todo: handle it in another place ?
          message.dateTime = new Date(message.dateTime);

          setMessages((prevMessages) => [...prevMessages, message]);
        } else {
          console.log("Message is from another group");
        }
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
    };

    await sendMessage(groupId ?? defaultGroupName, newMessage);
  };

  return (
    <div className="chat">
      <MessageList messages={messages} />
      <MessageForm onSend={handleSend} />
    </div>
  );
};

export default Chat;
