import { useState } from "react";
import axios from "axios";
import { ILanguage } from "../../models/ILanguage";

interface ICreateLanguageProps {
  onCreate: (newLanguage: ILanguage) => void;
}

export function CreateLanguage({ onCreate }: ICreateLanguageProps) {
  const [name, setName] = useState("");
  const [nativeName, setNativeName] = useState("");
  const [isConlang, setIsConlang] = useState(true);

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };
  const handleNativeNameChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setNativeName(event.target.value);
  };
  const handleIsConlangChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setIsConlang(event.target.checked);
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const response = await axios.post<ILanguage>(
      import.meta.env.VITE_API_URL + "languages",
      {
        authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
        name,
        nativeName,
        isConlang,
      }
    );

    console.log("Language created: ", response);
    setName("");
    setNativeName("");
    setIsConlang(true);

    onCreate(response.data);
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <input
          type="text"
          value={name}
          onChange={handleNameChange}
          placeholder="Language name"
          required
        />
      </div>
      <div>
        <input
          type="text"
          value={nativeName}
          onChange={handleNativeNameChange}
          placeholder="Language native name"
          required
        />
      </div>
      <div>
        <label>Is conlang</label>
        <input
          type="checkbox"
          checked={isConlang}
          onChange={handleIsConlangChange}
        />
      </div>
      <button>Create</button>
    </form>
  );
}
