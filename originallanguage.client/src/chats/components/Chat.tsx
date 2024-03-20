import React, { useEffect } from "react";
import MessageForm from "./MessageForm";
import { useSignalR } from "../SignalRContext";
import "./styles/Chat.css";
import { useChatMessages } from "../hooks/useChatMessages";
import MessageList from "./MessageList";

interface ChatProps {
  groupId?: string;
}

const defaultGroupName = "main";

const Chat: React.FC<ChatProps> = ({ groupId }: ChatProps) => {
  const { sendMessage } = useSignalR();
  const { messages, loadOlderMessages, isLoading, hasMoreMessages } =
    useChatMessages(groupId ?? defaultGroupName);

  const handleSend = async (content: string) => {
    await sendMessage(groupId ?? defaultGroupName, content);
  };

  useEffect(() => {
    loadOlderMessages();
  }, []);

  return (
    <div className="chat">
      <MessageList
        messages={messages}
        isLoading={isLoading}
        loadOlderMessages={loadOlderMessages}
        hasMoreMessages={hasMoreMessages}
      />
      <MessageForm onSend={handleSend} />
    </div>
  );
};

export default Chat;
