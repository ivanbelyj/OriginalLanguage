import React, { useState } from "react";
import LanguageCard from "../components/LanguageCard";
import { useLanguages } from "../hooks/useLanguages";
import { Typography, List } from "antd";
import { LanguageFilter } from "../components/LanguageFilter";

const LanguagesPage: React.FC = () => {
  const [isConlang, setIsConlang] = useState<boolean | null>(null);
  const { languages } = useLanguages({ isConlang });

  const { Title } = Typography;

  return (
    <div>
      <Title level={2}>Languages</Title>
      <LanguageFilter
        isConlang={isConlang}
        setIsConlang={(isConlang) => setIsConlang(isConlang)}
      />

      <List
        dataSource={languages}
        renderItem={(language) => (
          <List.Item>
            <LanguageCard language={language} style={{ width: "100%" }} />
          </List.Item>
        )}
      />
    </div>
  );
};

export default LanguagesPage;
