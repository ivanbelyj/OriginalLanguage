import React, { useEffect, useState } from "react";
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
import { Link, useNavigate } from "react-router-dom";
import { useJwtToken } from "../auth/AuthProvider";

const { Header, Content, Sider } = Layout;
const { Title } = Typography;

const UserProfilePage: React.FC = () => {
  // const { token, setToken } = useAuth();
  const navigate = useNavigate();

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
              about="Lorem ipsum dolor sit amet consectetur adipisicing elit. Qui distinctio accusamus, iusto ducimus perspiciatis eius eum illo assumenda id, quos sit ab. Labore obcaecati esse explicabo consequatur quam mollitia ullam!"
            />
            {/* Todo */}
            {/* <Link to="/logout">Logout</Link> */}
            <Button
              onClick={() => {
                // setToken(null);
                // navigate("/", { replace: true });
                navigate("/logout");
              }}
            >
              Logout
            </Button>
          </div>
        </Content>
      </Layout>
    </Layout>
  );
};

export default UserProfilePage;
