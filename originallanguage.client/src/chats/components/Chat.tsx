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
  const { messages, loadMessages, isLoading } = useChatMessages(
    groupId ?? defaultGroupName
  );
  console.log("messages: ", messages);

  const handleSend = async (content: string) => {
    const newMessage = {
      content,
    };

    await sendMessage(groupId ?? defaultGroupName, newMessage);
  };

  useEffect(() => {
    loadMessages(0);
  }, []);

  return (
    <div className="chat">
      <MessageList
        messages={messages}
        isLoading={isLoading}
        loadMessages={loadMessages}
      />
      <MessageForm onSend={handleSend} />
    </div>
  );
};

export default Chat;
