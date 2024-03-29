import { useEffect, useState } from "react";
import ILanguage from "../models/ILanguage";
import axios from "axios";

export type ICreateLanguage = Partial<ILanguage>;
export type IUpdateLanguage = Partial<ILanguage>;

export interface ILanguagesFilter {
  isConlang: boolean | null;
}

export function useLanguages(
  { isConlang }: ILanguagesFilter = { isConlang: null }
) {
  const [languages, setLanguages] = useState<ILanguage[]>([]);

  // function addLanguage(language: ILanguage) {
  //   setLanguages((prev) => {
  //     return [...prev, language];
  //   });
  // }

  async function postLanguage(language: ICreateLanguage): Promise<ILanguage> {
    const response = await axios.post<ILanguage>(
      import.meta.env.VITE_API_URL + "languages",
      language
    );

    console.log("Language post response: ", response);
    // addLanguage(response.data);
    setLanguages((prev) => {
      return [...prev, response.data];
    });

    return response.data;
  }

  async function updateLanguage(
    id: string,
    updateLanguage: IUpdateLanguage
  ): Promise<void> {
    await axios.put(
      import.meta.env.VITE_API_URL + "languages/" + id,
      updateLanguage
    );

    setLanguages((prev) => {
      return prev.map((language) =>
        language.id === id ? { ...language, ...updateLanguage } : language
      );
    });
  }

  async function deleteLanguage(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "languages/" + id);

    setLanguages((prev) => {
      return prev.filter((language) => language.id !== id);
    });
  }

  async function fetchLanguages() {
    const params = new URLSearchParams();
    // Todo: pagination
    params.append("limit", "1000");
    if (isConlang) {
      params.append("isConlang", String(isConlang));
    }
    const url =
      import.meta.env.VITE_API_URL + "languages/filtered?" + params.toString();

    console.log("fetch languages", url);

    fetch(url)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setLanguages(data))
      .catch((error) => {
        console.error("There has been a problem with languages fetch:", error);
      });
  }

  async function getLanguage(id: string): Promise<ILanguage> {
    const response = await axios.get<ILanguage>(
      import.meta.env.VITE_API_URL + "languages/" + id
    );

    return {
      ...response.data,
      dateTimeCreated: new Date(response.data.dateTimeCreated),
      dateTimeUpdated: new Date(response.data.dateTimeUpdated),
    };
  }

  useEffect(() => {
    fetchLanguages();
  }, [isConlang]);

  return {
    languages,
    postLanguage,
    updateLanguage,
    deleteLanguage,
    getLanguage,
  };
}
