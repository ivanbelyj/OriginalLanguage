import React from "react";
import { Layout, Button } from "antd";

import UserProfile from "../components/UserProfile";
import useLogout from "../../auth/hooks/useLogout";

const { Content } = Layout;

const UserProfilePage: React.FC = () => {
  const { logout } = useLogout();

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
                logout();
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
