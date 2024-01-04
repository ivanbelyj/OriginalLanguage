import React, { useState } from "react";
import {
  Layout,
  Menu,
  Avatar,
  Card,
  MenuProps,
  theme,
  Button,
  Typography,
} from "antd";

import UserProfile from "../components/UserProfile";

const { Header, Content, Sider } = Layout;
const { Title } = Typography;

const UserProfilePage: React.FC = () => {
  return (
    <Layout style={{ minHeight: "100%" }}>
      <Layout>
        {/* <Header
          style={{
            padding: 0,
            // background: colorBgContainer
          }}
        /> */}
        <Content style={{ margin: "0 16px" }}>
          <div
            style={{
              padding: 24,
              minHeight: 360,
              //   background: colorBgContainer,
              //   borderRadius: borderRadiusLG,
            }}
          >
            <UserProfile
              name="User"
              location="Voronezh"
              about="Lorem ipsum dolor sit amet consectetur adipisicing elit. Qui distinctio accusamus, iusto ducimus perspiciatis eius eum illo assumenda id, quos sit ab. Labore obcaecati esse explicabo consequatur quam mollitia ullam!"
            />
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default UserProfilePage;
