// src/components/MessageList.tsx
import React, { useEffect, useRef } from "react";
import { List, Avatar, Button } from "antd";
import IMessage from "../models/IMessage";
import MessageItem from "./MessageItem";
import "./MessageList.css";

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
