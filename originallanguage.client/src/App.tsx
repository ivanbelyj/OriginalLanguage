import { Navigation } from "./components/navigation";
import { Route, Routes } from "react-router-dom";
import AboutPage from "./pages/AboutPage";
import CoursesPage from "./pages/CoursesPage";
import MainPage from "./pages/MainPage";
import LanguagesPage from "./pages/LanguagesPage";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";

export default function App() {
  return (
    <>
      <Navigation />
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/about" element={<AboutPage />} />
        <Route path="/courses" element={<CoursesPage />} />
        <Route path="/languages" element={<LanguagesPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </>
  );
}
