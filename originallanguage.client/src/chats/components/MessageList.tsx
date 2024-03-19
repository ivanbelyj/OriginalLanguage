import React, { useState } from "react";
import IMessage from "../models/IMessage";
import MessageItem from "./MessageItem";
import InfiniteScroll from "react-infinite-scroll-component";
import { Divider, Skeleton } from "antd";
import "./styles/MessageList.css";

interface MessageListProps {
  messages: IMessage[];
  loadMessages: (page: number) => Promise<void>;
  isLoading: boolean;
}

const MessageList: React.FC<MessageListProps> = ({
  messages,
  loadMessages,
  isLoading,
}) => {
  const [page, setPage] = useState(0);
  const loadMore = () => {
    loadMessages(page);
    setPage((prev) => prev + 1);
  };
  return (
    <div id="scrollableDiv" className="message-list">
      <InfiniteScroll
        dataLength={messages.length}
        next={loadMore}
        hasMore={messages.length < 50}
        loader={
          <Skeleton
            avatar
            paragraph={{
              rows: 1,
            }}
            active
          />
        }
        endMessage={<Divider plain>It is all, no more messages</Divider>}
        scrollableTarget="scrollableDiv"
        style={{
          display: "flex",
          flexDirection: "column-reverse",
          overflow: "hidden",
        }}
        inverse={true}
      >
        {messages.map((item, index) => {
          return <MessageItem message={item} key={index} />;
        })}
      </InfiniteScroll>
    </div>
  );
};

export default MessageList;
