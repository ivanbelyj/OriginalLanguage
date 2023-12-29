import React, { useState } from "react";
import { ILanguage } from "../models/ILanguage";
import { CreateLanguage } from "../components/languages/create-language";
import { useLanguages } from "../hooks/languages";
import { LanguageCard } from "../components/languages/language-card";

const conlangOptionValue = "conlang";
const notConlangOptionValue = "not conlang";

const LanguagesPage: React.FC = () => {
  const [isConlang, setIsConlang] = useState<boolean | null>(null);
  const { languages, addLanguage } = useLanguages(isConlang);

  function handleCreateLanguage(newLanguage: ILanguage) {
    console.log("created language", newLanguage);
    addLanguage(newLanguage);
  }

  function toArtificialityOptionValue(value: boolean | null) {
    if (value === null) return "";
    return value ? conlangOptionValue : notConlangOptionValue;
  }

  function fromArtificialityOptionValue(value: string) {
    return value === conlangOptionValue
      ? true
      : value === notConlangOptionValue
      ? false
      : null;
  }

  return (
    <div>
      <h1>Languages</h1>
      <h2>Create language</h2>
      <CreateLanguage onCreate={handleCreateLanguage} />

      <h2>Languages</h2>
      <label>
        Artificiality
        <select
          value={toArtificialityOptionValue(isConlang)}
          onChange={(event) => {
            setIsConlang(fromArtificialityOptionValue(event.target.value));
          }}
        >
          <option value="">Any</option>
          <option value={conlangOptionValue}>Conlang</option>
          <option value={notConlangOptionValue}>Not Conlang</option>
        </select>
      </label>
      {languages.map((language) => {
        return <LanguageCard language={language} key={language.id} />;
      })}
    </div>
  );
};

export default LanguagesPage;
