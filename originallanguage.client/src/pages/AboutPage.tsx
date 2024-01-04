import React from "react";
import { Typography } from "antd";

const { Title, Paragraph, Text } = Typography;

export const AboutPage: React.FC = () => {
  return (
    <div>
      <Title level={2} style={{ marginBottom: "1em" }}>
        About
      </Title>
      <Paragraph>Original Language</Paragraph>
    </div>
  );
};

export default AboutPage;
