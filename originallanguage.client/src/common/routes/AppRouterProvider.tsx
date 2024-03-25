import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useJwtToken } from "../../auth/AuthProvider";
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
import ArticlesPage from "../pages/ArticlesPage";
import ArticlePage from "../pages/ArticlePage";
import UserProfilePage from "../../user/pages/UserProfilePage";
import EditCoursePage from "../../courses/pages/EditCoursePage";
import EditLanguagePage from "../../languages/pages/EditLanguagePage";
import Logout from "../../auth/pages/LogoutPage";
import LessonPlayerPage from "../../courses/pages/LessonPlayerPage";
import CourseLessonsPage from "../../courses/pages/CourseLessonsPage";
import CourseFullInfo from "../../courses/components/CourseFullInfo";

const AppRouterProvider = () => {
  const { token } = useJwtToken();

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
      path: "/contact",
      element: <ContactPage />,
    },
    {
      path: "/register",
      element: <RegisterPage />,
    },
    {
      path: "/articles",
      element: <ArticlesPage />,
    },
    {
      path: "/article",
      element: <ArticlePage />,
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
          path: "/edit-course/:id",
          element: <EditCoursePage />,
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
          path: "/course/:id/lessons/",
          element: <CourseLessonsPage />,
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
