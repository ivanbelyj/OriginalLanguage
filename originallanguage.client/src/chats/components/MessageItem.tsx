import React from "react";
import { List, Avatar } from "antd";
import IMessage from "../models/IMessage";
import "./MessageItem.css";

interface MessageItemProps {
  message: IMessage;
}

const MessageItem: React.FC<MessageItemProps> = ({ message }) => {
  // Todo: dateTime in message
  const formattedDateTime = new Date().toLocaleString([], {
    year: "numeric",
    month: "short",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });

  return (
    <List.Item className="message-item">
      <List.Item.Meta
        avatar={<Avatar src={""} />}
        title={"Author"}
        description={
          <div style={{ fontSize: "14px", color: "#333" }}>
            {message.content}
            <span style={{ fontSize: "12px", color: "#999", float: "right" }}>
              {formattedDateTime}
            </span>
          </div>
        }
      />
    </List.Item>
  );
};

export default MessageItem;
