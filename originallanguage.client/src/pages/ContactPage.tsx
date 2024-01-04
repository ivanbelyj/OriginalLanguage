import { Card, Typography } from "antd";

const { Title, Paragraph, Text } = Typography;
const { Meta } = Card;

export default function ContactPage() {
  return (
    <div>
      <Title level={2}>Contact us</Title>
      <Paragraph>Developed by Ivan Belyj</Paragraph>
    </div>
  );
}
