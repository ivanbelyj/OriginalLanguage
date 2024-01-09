import { useEffect, useState } from "react";
import ILanguage from "../models/ILanguage";
import axios, { AxiosResponse } from "axios";

export function useUserLanguages({ authorId }: { authorId: string }) {
  const [userLanguages, setUserLanguages] = useState<ILanguage[]>([]);

  function addLanguage(newLanguage: ILanguage) {
    setUserLanguages([...userLanguages, newLanguage]);
  }

  function removeLanguage(langToRemove: ILanguage) {
    setUserLanguages(
      userLanguages.filter((lang) => lang.id !== langToRemove.id)
    );
  }

  async function fetchLanguages() {
    const url = import.meta.env.VITE_API_URL + `accounts/${authorId}/languages`;

    console.log("fetch languages", url);

    try {
      const response: AxiosResponse<ILanguage[]> = await axios.get<ILanguage[]>(
        url
      );

      if (!(response.status === 200)) {
        throw new Error("Network response was not ok");
      }
      setUserLanguages(response.data);
    } catch (error: any) {
      console.error(
        "There has been a problem with user languages fetch:",
        error
      );
    }
  }

  useEffect(() => {
    fetchLanguages();
  }, [authorId]);

  return { userLanguages, addLanguage, removeLanguage };
}
