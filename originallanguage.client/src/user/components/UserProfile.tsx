import React from "react";
import { Avatar, Typography, Space } from "antd";
import { UserOutlined } from "@ant-design/icons";

const { Text, Title } = Typography;

interface UserProfileProps {
  name: string;
  avatarUrl?: string;
  about?: string;
  location?: string;
}

const UserProfile: React.FC<UserProfileProps> = ({
  name,
  avatarUrl,
  about: about,
  location,
}) => {
  return (
    <div>
      <Space direction="vertical" align="center" size={16}>
        <Avatar
          src={avatarUrl}
          size={128}
          icon={avatarUrl ? null : <UserOutlined />}
        ></Avatar>
        <Title level={2}>{name}</Title>
        <Text>{about}</Text>
        <Text>{location}</Text>
      </Space>
    </div>
  );
};

export default UserProfile;
