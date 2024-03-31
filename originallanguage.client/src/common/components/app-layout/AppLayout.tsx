import { Layout } from "antd";

import AppHeader from "./AppHeader";
import { Link, Outlet } from "react-router-dom";
import AppSider from "./AppSider";
import { useJwtToken } from "../../../auth/AuthProvider";

const { Footer, Content } = Layout;

export default function AppLayout() {
  const { token } = useJwtToken();

  const currentYear = new Date().getFullYear();
  return (
    <Layout style={{ minHeight: "100vh" }}>
      {token && <AppSider />}

      <Layout style={{ marginLeft: "80px" }}>
        <AppHeader />

        <Content style={{ padding: "1.5em" }}>
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
          Original Language ©2024{currentYear == 2024 ? "" : `-${currentYear}`}.
          <Link to="/contact"> Contact us</Link>
        </Footer>
      </Layout>
    </Layout>
  );
}
