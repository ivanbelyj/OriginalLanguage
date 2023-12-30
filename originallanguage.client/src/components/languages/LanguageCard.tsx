import { ILanguage } from "../../models/ILanguage";
import { Card, Typography, Descriptions } from "antd";

const { Text, Paragraph } = Typography;

export const LanguageCard: React.FC<{ language: ILanguage }> = ({
  language,
}) => {
  return (
    <Card>
      <Paragraph strong>
        <Text>{language.name}</Text>
      </Paragraph>
      <Descriptions bordered>
        <Descriptions.Item label="Native name">
          {language.nativeName}
        </Descriptions.Item>
        <Descriptions.Item label="Author id">
          {language.authorId}
        </Descriptions.Item>
        {language.isConlang && (
          <Descriptions.Item label="Type">
            Constructed language
          </Descriptions.Item>
        )}
        <Descriptions.Item label="Date added">
          {language.dateTimeAdded.toString()}
        </Descriptions.Item>
      </Descriptions>
    </Card>
  );
};
