// src/components/MessageList.tsx
import React, { useEffect } from "react";
import { List, Avatar } from "antd";
import IMessage from "../../models/chats/IMessage";

interface MessageListProps {
  messages: IMessage[];
}

const MessageList: React.FC<MessageListProps> = ({ messages }) => {
  return (
    <List
      itemLayout="horizontal"
      dataSource={messages}
      renderItem={(message: IMessage) => (
        <List.Item>
          <List.Item.Meta
            //   avatar={<Avatar src={message.avatar} />}
            //   title={message.author}
            description={message.content}
          />
        </List.Item>
      )}
    />
  );
};

export default MessageList;
