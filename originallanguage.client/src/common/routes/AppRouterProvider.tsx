import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../../auth/AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import MainPage from "../pages/MainPage";
import AboutPage from "../pages/AboutPage";
import CoursesPage from "../../courses/pages/CoursesPage";
import LanguagesPage from "../../languages/pages/LanguagesPage";
import LoginPage from "../../auth/pages/LoginPage";
import RegisterPage from "../../auth/pages/RegisterPage";
import AppLayout from "../components/app-layout/AppLayout";
import ContactPage from "../pages/ContactPage";
import LanguageFullInfo from "../../languages/components/LanguageFullInfo";
import UserProfilePage from "../../user/pages/UserProfilePage";
import ManageCoursePage from "../../courses/pages/ManageCoursePage";
import EditLanguagePage from "../../languages/pages/EditLanguagePage";
import Logout from "../../auth/pages/LogoutPage";
import LessonPlayerPage from "../../lesson-player/pages/LessonPlayerPage";
import CoursePage from "../../courses/pages/CoursePage";
import CourseFullInfo from "../../courses/components/CourseFullInfo";
import ManageArticlePage from "../../articles/pages/ManageArticlePage";
import ArticlesPage from "../../articles/pages/ArticlesPage";

const AppRouterProvider = () => {
  const { token } = useAuth();

  const routesForPublic = [
    {
      path: "/",
      element: <MainPage />,
    },
    {
      path: "/about",
      element: <AboutPage />,
    },
    {
      path: "/contact",
      element: <ContactPage />,
    },
    {
      path: "/register",
      element: <RegisterPage />,
    },
    {
      path: "/courses",
      element: <CoursesPage />,
    },

    {
      path: "/courses/:id",
      element: <CourseFullInfo />,
    },
    {
      path: "/languages",
      element: <LanguagesPage />,
    },
    {
      path: "/language/:id",
      element: <LanguageFullInfo />,
    },
    {
      path: "/user/:id/articles",
      element: <ArticlesPage />,
    },
  ];

  const routesForAuthenticatedOnly = [
    {
      path: "/",
      element: <ProtectedRoute />,
      children: [
        {
          path: "/profile",
          element: <UserProfilePage />,
        },
        {
          path: "/logout",
          element: <Logout />,
        },
        {
          path: "/manage-course/:id",
          element: <ManageCoursePage />,
        },
        {
          path: "/manage-course/:id/:activeTab",
          element: <ManageCoursePage />,
        },
        {
          path: "/edit-language/:id",
          element: <EditLanguagePage />,
        },
        {
          path: "/lessons/:id/player/",
          element: <LessonPlayerPage />,
        },
        {
          path: "/courses/:id/lessons/",
          element: <CoursePage />,
        },
        {
          path: "/articles/:id/manage",
          element: <ManageArticlePage />,
        },
      ],
    },
  ];

  const routesForNotAuthenticatedOnly = [
    {
      path: "/login",
      element: <LoginPage />,
    },
  ];

  const layoutChildren = [
    ...routesForPublic,
    ...(!token ? routesForNotAuthenticatedOnly : []),
    ...routesForAuthenticatedOnly,
  ];

  const router = createBrowserRouter([
    {
      path: "",
      element: <AppLayout />,
      children: layoutChildren,
    },
  ]);

  return <RouterProvider router={router} />;
};

export default AppRouterProvider;
