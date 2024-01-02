import { useState } from "react";
import axios from "axios";
import ILanguage from "../../models/ILanguage";
import { Form, Input, Checkbox, Button } from "antd";
import { CheckboxChangeEvent } from "antd/es/checkbox";

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
  const handleIsConlangChange = (event: CheckboxChangeEvent) => {
    setIsConlang(event.target.checked);
  };

  const handleFinish = async (_: React.FormEvent) => {
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
    // setName("");
    // setNativeName("");
    // setIsConlang(true);

    onCreate(response.data);
  };

  return (
    <Form onFinish={handleFinish} style={{ maxWidth: "300px" }}>
      <Form.Item
        label="Language name"
        name="name"
        rules={[{ required: true, message: "Please enter the language name" }]}
      >
        <Input type="text" value={name} onChange={handleNameChange} />
      </Form.Item>
      <Form.Item
        label="Language native name"
        name="native-name"
        rules={[
          { required: true, message: "Please enter the language native name" },
        ]}
      >
        <Input
          type="text"
          value={nativeName}
          onChange={handleNativeNameChange}
        />
      </Form.Item>
      <Form.Item label="Is conlang" name="is-conlang">
        <Checkbox checked={isConlang} onChange={handleIsConlangChange} />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Create
        </Button>
      </Form.Item>
    </Form>
  );
}
