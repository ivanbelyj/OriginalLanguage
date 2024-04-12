import { UserOutlined } from "@ant-design/icons";
import { Avatar, Card, Typography } from "antd";
import React from "react";
import { Link } from "react-router-dom";

const { Paragraph } = Typography;

export interface IContentCardProps {}

export const ContentCard: React.FC<IContentCardProps> = ({}) => {
  return (
    <Card
      title={
        <>
          <Link to={""}>Todo: content name and link</Link>
        </>
      }
    >
      <Paragraph>Todo: card content</Paragraph>

      <Link to="/">
        <Card.Meta
          avatar={<Avatar icon={<UserOutlined />}>U</Avatar>}
          title="User name"
        />
      </Link>
    </Card>
  );
};
