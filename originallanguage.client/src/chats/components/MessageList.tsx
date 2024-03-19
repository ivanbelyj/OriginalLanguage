import React, { useState } from "react";
import IMessage from "../models/IMessage";
import MessageItem from "./MessageItem";
import InfiniteScroll from "react-infinite-scroll-component";
import { Divider, Skeleton } from "antd";
import "./styles/MessageList.css";

interface MessageListProps {
  messages: IMessage[];
  loadOlderMessages: () => Promise<void>;
  isLoading: boolean;
}

const MessageList: React.FC<MessageListProps> = ({
  messages,
  loadOlderMessages,
  isLoading,
}) => {
  console.log("messages passed to message list", messages);
  const loadMore = () => {
    loadOlderMessages();
  };
  return (
    <div id="scrollableDiv" className="message-list">
      <InfiniteScroll
        dataLength={messages.length}
        next={loadMore}
        hasMore={true} // Todo: has more
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
        style={
          {
            // display: "flex",
            // flexDirection: "column-reverse",
            // overflow: "hidden",
          }
        }
        inverse={true}
      >
        {messages.map((item, index) => {
          return <MessageItem message={item} key={item.id} />;
        })}
      </InfiniteScroll>
    </div>
  );
};

export default MessageList;
