import { Avatar, Card, Typography } from "antd";
import ILanguage from "../models/ILanguage";
import LanguageFlag from "./LanguageFlag";
import { Link } from "react-router-dom";
import React from "react";

const { Meta } = Card;
const { Paragraph } = Typography;

interface ILanguageInfoProps {
  language: ILanguage;
  style?: React.CSSProperties;
}

function cropWithEllipsis(str: string) {
  const maxLength = 200;
  return str.slice(0, maxLength) + (str.length - 3 > maxLength ? "..." : "");
}

export default function LanguageCard({ language, style }: ILanguageInfoProps) {
  return (
    <Card
      style={style}
      title={
        <>
          <Link to={"/language/" + language.id}>
            <LanguageFlag /> {language.name}
          </Link>
        </>
      }
    >
      <Paragraph>
        {language.about ? cropWithEllipsis(language.about) : ""}
      </Paragraph>

      <Meta
        avatar={<Avatar>U</Avatar>}
        title="User name"
        description="Some description"
      />
    </Card>
  );
}
