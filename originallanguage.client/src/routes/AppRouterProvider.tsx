import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useJwtToken } from "../auth/AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import MainPage from "../pages/MainPage";
import AboutPage from "../pages/AboutPage";
import CoursesPage from "../pages/courses/CoursesPage";
import LanguagesPage from "../pages/LanguagesPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import AppLayout from "../app-layout/AppLayout";
import ContactPage from "../pages/ContactPage";
import LanguageFullInfo from "../components/languages/LanguageFullInfo";
import ArticlesPage from "../pages/ArticlesPage";
import ArticlePage from "../pages/ArticlePage";
import UserProfilePage from "../pages/UserProfilePage";
import EditCoursePage from "../pages/courses/EditCoursePage";
import EditLanguagePage from "../pages/EditLanguagePage";
import Logout from "../auth/Logout";
import CourseFullInfo from "../components/courses/CourseFullInfo";
import LessonPlayerPage from "../pages/courses/LessonPlayerPage";
import CourseLessonsPage from "../pages/courses/CourseLessonsPage";

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
