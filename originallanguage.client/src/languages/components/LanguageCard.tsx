import { Avatar, Card, Typography } from "antd";
import ILanguage from "../models/ILanguage";
import LanguageFlag from "./LanguageFlag";
import { Link } from "react-router-dom";
import React from "react";
import StringUtils from "../../common/utils/string-utils";
import { UserOutlined } from "@ant-design/icons";

const { Meta } = Card;
const { Paragraph } = Typography;

interface ILanguageInfoProps {
  language: ILanguage;
  style?: React.CSSProperties;
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
        {language.about
          ? StringUtils.cropWithEllipsis(language.about, 200)
          : ""}
      </Paragraph>

      <Link to="/">
        <Meta
          avatar={<Avatar icon={<UserOutlined />}>U</Avatar>}
          title="User name"
        />
      </Link>
    </Card>
  );
}
