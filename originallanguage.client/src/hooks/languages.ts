import { useEffect, useState } from "react";
import { ILanguage } from "../models/ILanguage";

export function useLanguages(isConlang: boolean | null) {
  const [languages, setLanguages] = useState<ILanguage[]>([]);

  function addLanguage(language: ILanguage) {
    setLanguages((prev) => {
      return [...prev, language];
    });
  }

  async function fetchLanguages() {
    const params = new URLSearchParams();
    if (isConlang !== null) {
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

  useEffect(() => {
    fetchLanguages();
  }, [isConlang]);

  return { languages, addLanguage };
}
