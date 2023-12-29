import { ILanguage } from "../../models/ILanguage";

export const LanguageCard: React.FC<{ language: ILanguage }> = ({
  language,
}) => {
  return (
    <div>
      <h3>{language.name}</h3>
      <div>Native name: {language.nativeName}</div>
      <div>Author id: {language.authorId}</div>
      {language.isConlang && <div>Constructed language</div>}
      <div>Date added: {language.dateTimeAdded.toString()}</div>
    </div>
  );
};
