import { Layout } from "antd";

import AppHeader from "./AppHeader";
import { Link, Outlet } from "react-router-dom";
import AppSider from "./AppSider";
import { relative } from "path";

const { Footer, Content } = Layout;
// const { Text, Paragraph } = Typography;

export default function AppLayout() {
  const currentYear = new Date().getFullYear();
  return (
    <Layout style={{ minHeight: "100vh" }}>
      <AppHeader />
      <Layout>
        <AppSider />
        <Content style={{ padding: "1.5em", backgroundColor: "#f0f2f5" }}>
          <Outlet />
        </Content>
      </Layout>

      <Footer
        style={{
          paddingLeft: "1.5em",
          paddingRight: "1.5em",
          backgroundColor: "#141414",
          color: "#ffffff",
        }}
      >
        Original Language ©2024{currentYear == 2024 ? "" : `-${currentYear}`}.
        <Link to="/contact"> Contact us</Link>
      </Footer>
    </Layout>
  );
}
