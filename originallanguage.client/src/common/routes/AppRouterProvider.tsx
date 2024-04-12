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
import RouteUtils from "./RouteUtils";

const AppRouterProvider = () => {
  const { token } = useAuth();

  const routesForPublic = [
    {
      path: RouteUtils.main(),
      element: <MainPage />,
    },
    {
      path: RouteUtils.about(),
      element: <AboutPage />,
    },
    {
      path: RouteUtils.contact(),
      element: <ContactPage />,
    },
    {
      path: RouteUtils.register(),
      element: <RegisterPage />,
    },
    {
      path: RouteUtils.courses(),
      element: <CoursesPage />,
    },
    {
      path: RouteUtils.course(),
      element: <CourseFullInfo />,
    },
    {
      path: RouteUtils.languages(),
      element: <LanguagesPage />,
    },
    {
      path: RouteUtils.language(),
      element: <LanguageFullInfo />,
    },
    {
      path: RouteUtils.userArticles(),
      element: <ArticlesPage />,
    },
  ];

  const routesForAuthenticatedOnly = [
    {
      path: "/",
      element: <ProtectedRoute />,
      children: [
        {
          path: RouteUtils.profile(),
          element: <UserProfilePage />,
        },
        {
          path: RouteUtils.logout(),
          element: <Logout />,
        },
        {
          path: RouteUtils.manageCourseDefault(),
          element: <ManageCoursePage />,
        },
        {
          path: RouteUtils.manageCourse(),
          element: <ManageCoursePage />,
        },
        {
          path: RouteUtils.editLanguage(),
          element: <EditLanguagePage />,
        },
        {
          path: RouteUtils.lessonPlayer(),
          element: <LessonPlayerPage />,
        },
        {
          path: RouteUtils.courseLessons(),
          element: <CoursePage />,
        },
        {
          path: RouteUtils.manageArticle(),
          element: <ManageArticlePage />,
        },
      ],
    },
  ];

  const routesForNotAuthenticatedOnly = [
    {
      path: RouteUtils.login(),
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
