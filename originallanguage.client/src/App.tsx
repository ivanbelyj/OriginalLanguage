import { Layout, Menu } from "antd";
import { Routes, Route } from "react-router-dom";
import AboutPage from "./pages/AboutPage";
import CoursesPage from "./pages/CoursesPage";
import MainPage from "./pages/MainPage";
import LanguagesPage from "./pages/LanguagesPage";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";
import AppHeader from "./components/AppHeader";

const { Footer, Content } = Layout;

export default function App() {
  return (
    <Layout style={{ minHeight: "100vh" }}>
      <AppHeader />
      <Content style={{ padding: "1.5em", backgroundColor: "#f0f2f5" }}>
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/about" element={<AboutPage />} />
          <Route path="/courses" element={<CoursesPage />} />
          <Route path="/languages" element={<LanguagesPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Routes>
      </Content>
      <Footer
        style={{
          paddingLeft: "1.5em",
          paddingRight: "1.5em",
          backgroundColor: "#141414",
          color: "#ffffff",
        }}
      >
        Original Language ©2024
      </Footer>
    </Layout>
  );
}
