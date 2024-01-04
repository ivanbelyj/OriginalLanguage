import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";
import { ProtectedRoute } from "./ProtectedRoute";
import MainPage from "../pages/MainPage";
import AboutPage from "../pages/AboutPage";
import CoursesPage from "../pages/CoursesPage";
import LanguagesPage from "../pages/LanguagesPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import AppLayout from "../app-layout/AppLayout";
import ContactPage from "../pages/ContactPage";
import LanguagePage from "../pages/LanguagePage";
import ArticlesPage from "../pages/ArticlesPage";
import ArticlePage from "../pages/ArticlePage";
import UserProfilePage from "../pages/UserProfilePage";
import EditCoursePage from "../pages/EditCoursePage";
import EditLanguagePage from "../pages/EditLanguagePage";

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
      path: "/courses",
      element: <CoursesPage />,
    },
    {
      path: "/languages",
      element: <LanguagesPage />,
    },
    {
      path: "/language",
      element: <LanguagePage />,
    },
    // {
    //   path: "/login",
    //   element: <LoginPage />,
    // },
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
          element: <div>Logout</div>,
        },
        {
          path: "/edit-course/:id",
          element: <EditCoursePage />,
        },
        {
          path: "/edit-language/:id",
          element: <EditLanguagePage />,
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
