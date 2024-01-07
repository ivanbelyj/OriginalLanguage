import { Card, Descriptions, Typography } from "antd";
import ILanguage from "../../models/ILanguage";
import LanguageFlag from "./LanguageFlag";
import { Link } from "react-router-dom";

const { Meta } = Card;
const { Text } = Typography;

interface ILanguageInfoProps {
  language: ILanguage;
}

function cropWithEllipsis(str: string) {
  const maxLength = 200;
  return str.slice(0, maxLength) + (str.length - 3 > maxLength ? "..." : "");
}

export default function LanguageCard({ language }: ILanguageInfoProps) {
  return (
    <Card
      title={
        <>
          <Link to={"/language/" + language.id}>
            <LanguageFlag /> {language.name}
          </Link>
        </>
      }
    >
      <Text>{language.about ? cropWithEllipsis(language.about) : ""}</Text>

      {/* <Meta
        avatar={
          <LanguageFlag src="https://xsgames.co/randomusers/avatar.php?g=pixel" />
        }
        title="Card title"
        description="This is the description"
      /> */}
    </Card>
  );
}
