import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { IArticle } from "../../articles/models/models";

export function useUserArticles({ authorId }: { authorId?: string }) {
  const [userArticles, setUserArticles] = useState<IArticle[]>([]);

  function addArticle(newArticle: IArticle) {
    setUserArticles([...userArticles, newArticle]);
  }

  function removeArticle(articleToRemove: IArticle) {
    setUserArticles(
      userArticles.filter((article) => article.id !== articleToRemove.id)
    );
  }

  async function fetchArticles() {
    if (!authorId) return;

    const url = import.meta.env.VITE_API_URL + `accounts/${authorId}/articles`;

    console.log("fetch articles", url);

    try {
      const response: AxiosResponse<IArticle[]> = await axios.get<IArticle[]>(
        url
      );

      if (!(response.status === 200)) {
        throw new Error("Network response was not ok");
      }
      setUserArticles(response.data);
    } catch (error: any) {
      console.error(
        "There has been a problem with user articles fetch:",
        error
      );
    }
  }

  useEffect(() => {
    fetchArticles();
  }, [authorId]);

  return { userArticles, addArticle, removeArticle };
}
