import React, { useState } from "react";
import { ILanguage } from "../models/ILanguage";
import { EditLanguage } from "../components/languages/EditLanguage";
import { LanguageCard } from "../components/languages/LanguageCard";
import { useLanguages } from "../hooks/languages";
import { Typography, Select, Card, List } from "antd";

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

  const { Title } = Typography;
  const { Option } = Select;

  return (
    <Card>
      <Title level={2}>Languages</Title>
      <Title level={3}>Create language</Title>
      <EditLanguage onCreate={handleCreateLanguage} />

      <Title level={3}>Languages</Title>
      <Select
        value={toArtificialityOptionValue(isConlang)}
        onChange={(value) => {
          setIsConlang(fromArtificialityOptionValue(value));
        }}
        style={{ width: "100%" }}
      >
        <Option value="">Any</Option>
        <Option value={conlangOptionValue}>Conlang</Option>
        <Option value={notConlangOptionValue}>Not Conlang</Option>
      </Select>

      <List
        dataSource={languages}
        renderItem={(language) => (
          <List.Item>
            <LanguageCard language={language} />
          </List.Item>
        )}
      />
    </Card>
  );
};

export default LanguagesPage;
