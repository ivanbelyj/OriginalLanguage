import { Layout } from "antd";

import AppHeader from "./components/AppHeader";
import { Outlet } from "react-router-dom";

const { Footer, Content } = Layout;

export default function AppLayout() {
  const currentYear = new Date().getFullYear();
  return (
    <Layout style={{ minHeight: "100vh" }}>
      <AppHeader />
      <Content style={{ padding: "1.5em", backgroundColor: "#f0f2f5" }}>
        <Outlet />
      </Content>
      <Footer
        style={{
          paddingLeft: "1.5em",
          paddingRight: "1.5em",
          backgroundColor: "#141414",
          color: "#ffffff",
        }}
      >
        Original Language ©2024{currentYear == 2024 ? "" : `-${currentYear}`}
      </Footer>
    </Layout>
  );
}
