import { useEffect, useState } from "react";
import { useSignalR } from "../SignalRContext";
import IMessage from "../models/IMessage";
import axios from "axios";

const messagesPerPage = 50;

export function useChatMessages(groupId: string) {
  const { connection, joinGroup } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [hasMoreMessages, setHasMoreMessages] = useState(true);
  const [oldestMessageId, setOldestMessageId] = useState<number | null>(null);

  useEffect(() => {
    console.log("connection", connection);
    if (connection?.state === "Connected") initChat();
    else if (connection?.state === "Disconnected")
      connection?.start().then(() => initChat());

    return () => {
      if (connection) {
        connection.off("ReceiveMessage");
      }
    };
  }, [connection, groupId]);

  function initChat() {
    if (connection) {
      console.log("Connection state: ", connection.state);
      console.log("Connection state: ", connection.state);

      connection.on("ReceiveMessage", (message: IMessage) => {
        if (message.groupId == groupId) {
          addMessages([message]);
        } else {
        }
      });

      joinGroup(groupId);
    }
  }

  async function getMessages(idLimit: number | null, limit: number) {
    try {
      setIsLoading(true);
      const response = await axios.get(
        `${import.meta.env.VITE_API_URL}chat-messages`,
        {
          params: { groupId, idLimit, limit },
        }
      );
      setIsLoading(false);
      setHasMoreMessages(response.data.length === messagesPerPage);
      return response.data.reverse();
    } catch (error) {
      console.error("Error fetching messages:", error);
    }
  }

  const addMessages = (newMessages: IMessage[]) => {
    const newMessagesToSet = newMessages.map((message) => {
      message.dateTime = new Date(message.dateTime);
      return message;
    });

    setMessages((prevMessages) =>
      [...newMessagesToSet, ...prevMessages].sort(
        (m1, m2) => m1.dateTime.getTime() - m2.dateTime.getTime()
      )
    );
  };

  useEffect(() => {
    const oldestMessage = messages.reduce(
      (min, current) => (current.id < min.id ? current : min),
      messages[0]
    );
    if (oldestMessage) {
      setOldestMessageId(oldestMessage.id);
    }
  }, [messages]);

  const loadOlderMessages = async () => {
    const newMessages = await getMessages(oldestMessageId, messagesPerPage);
    addMessages(newMessages);
  };

  return { messages, loadOlderMessages, isLoading, hasMoreMessages };
}
