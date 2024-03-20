import React from "react";
import { List, Avatar } from "antd";
import IMessage from "../models/IMessage";
import "./styles/MessageItem.css";

interface MessageItemProps {
  message: IMessage;
}

const isCurrentYear = (date: Date) => {
  return date.getFullYear() === new Date().getFullYear();
};

const MessageItem: React.FC<MessageItemProps> = ({ message }) => {
  const formattedDateTime = message.dateTime.toLocaleString([], {
    ...(isCurrentYear(message.dateTime) ? {} : { year: "numeric" }),
    month: "short",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });

  return (
    <div className="message">
      <Avatar className="message__avatar" src={message.avatarUrl} />
      <div className="message__content-box">
        <a href="https://ant.design" className="message__user-name">
          {message.userName}
        </a>
        <span className="message__date-time">{formattedDateTime}</span>
        <div className="message__content">{message.content}</div>
      </div>
    </div>
  );
};

export default MessageItem;
