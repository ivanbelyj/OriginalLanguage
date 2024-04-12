import { useEffect, useState } from "react";
import axios from "axios";
import { IArticle } from "../models/models";

export interface ICreateArticle {
  authorId: string;
  title?: string;
  content?: string;
  shortDescription?: string;
}

export interface IUpdateArticle {
  authorId: string;
  title?: string;
  content?: string;
  shortDescription?: string;
}

export function useArticles() {
  const [articles, setArticles] = useState<IArticle[]>([]);

  async function postArticle(article: ICreateArticle): Promise<IArticle> {
    const response = await axios.post<IArticle>(
      import.meta.env.VITE_API_URL + "articles",
      article
    );

    setArticles((prev) => [...prev, response.data]);

    return response.data;
  }

  async function updateArticle(
    id: string,
    updateArticle: IUpdateArticle
  ): Promise<void> {
    await axios.put<IArticle>(
      import.meta.env.VITE_API_URL + "articles/" + id,
      updateArticle
    );

    setArticles((prev) =>
      prev.map((article) =>
        article.id === id ? { ...article, ...updateArticle } : article
      )
    );
  }

  async function deleteArticle(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "articles/" + id);

    setArticles((prev) => prev.filter((article) => article.id !== id));
  }

  async function fetchArticles() {
    try {
      const response = await axios.get<IArticle[]>(
        import.meta.env.VITE_API_URL + "articles"
      );
      setArticles(response.data);
    } catch (error) {
      console.error("There has been a problem with articles fetch:", error);
    }
  }

  async function getArticle(id: string): Promise<IArticle> {
    const response = await axios.get<IArticle>(
      import.meta.env.VITE_API_URL + "articles/" + id
    );

    return {
      ...response.data,
      dateTimeAdded: new Date(response.data.dateTimeAdded),
    };
  }

  useEffect(() => {
    fetchArticles();
  }, []);

  return {
    articles,
    getArticle,
    postArticle,
    updateArticle,
    deleteArticle,
  };
}
