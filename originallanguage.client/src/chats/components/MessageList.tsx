// src/components/MessageList.tsx
import React from "react";
import { List } from "antd";
import IMessage from "../models/IMessage";
import MessageItem from "./MessageItem";
import "./styles/MessageList.css";

interface MessageListProps {
  messages: IMessage[];
}

const MessageList: React.FC<MessageListProps> = ({ messages }) => {
  return (
    <List
      className="message-list"
      itemLayout="horizontal"
      dataSource={messages}
      renderItem={(message: IMessage) => <MessageItem message={message} />}
    />
  );
};

export default MessageList;