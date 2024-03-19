import { useEffect, useState } from "react";
import axios from "axios";
import { useSignalR } from "../SignalRContext";
import IMessage from "../models/IMessage";

export function useChatMessages(groupId: string) {
  const messagesPerPage = 10;

  const { connection, joinGroup } = useSignalR();
  const [messages, setMessages] = useState<IMessage[]>([]);
  // const [hasMoreItems, setHasMoreItems] = useState(true);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    console.log("connection", connection);
    if (connection) {
      console.log("Connection state: ", connection.state);

      connection.on("ReceiveMessage", (message: IMessage) => {
        console.log("Receive message!", message);
        if (message.groupId == groupId) {
          addMessages([message]);
          // setMessages((prevMessages) => [...prevMessages, message]);
        } else {
          console.log("Message is from another group");
        }
      });

      setTimeout(() => joinGroup(groupId), 1000);
    }

    return () => {
      if (connection) {
        connection.off("ReceiveMessage");
      }
    };
  }, [connection, groupId]);

  async function getMessages(page: number) {
    const offset = (page - 1) * messagesPerPage;
    const limit = messagesPerPage;

    try {
      setIsLoading(true);
      const response = await axios.get(
        `${import.meta.env.VITE_API_URL}chat-messages`,
        {
          params: { limit, offset, groupId },
        }
      );
      setIsLoading(false);
      return response.data;
    } catch (error) {
      console.error("Error fetching messages:", error);
    }
  }

  const addMessages = (messages: IMessage[]) => {
    setMessages((prevMessages) => [
      ...prevMessages,
      ...messages
        .map((message) => {
          message.dateTime = new Date(message.dateTime);
          return message;
        })
        .reverse(),
    ]);
  };

  const loadMessages = async (page: number) => {
    addMessages(await getMessages(page));
  };

  return { messages, loadMessages, isLoading };
}
