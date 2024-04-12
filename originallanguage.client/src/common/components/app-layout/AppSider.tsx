import { Layout, Menu, MenuProps, Tooltip } from "antd";

import {
  BookOutlined,
  TranslationOutlined,
  UserOutlined,
  PlusCircleOutlined,
  ReadOutlined,
} from "@ant-design/icons";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLanguages } from "../../../languages/hooks/useLanguages";
import { useCourses } from "../../../courses/hooks/useCourses";
import { useUserLanguages } from "../../../user/hooks/useUserLanguages";
import { useAuth } from "../../../auth/AuthProvider";
import { useUserCourses } from "../../../user/hooks/useUserCourses";
import LanguageUtils from "../../../languages/language-utils";
import CourseUtils from "../../../courses/course-utils";
import { useUserArticles } from "../../../user/hooks/useUserArticles";
import { useArticles } from "../../../articles/hooks/useArticles";
import ArticleUtils from "../../../articles/article-utils";
import RouteUtils from "../../routes/RouteUtils";

const { Sider } = Layout;

type MenuItem = Required<MenuProps>["items"][number];

function createItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[]
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
  } as MenuItem;
}

const AppSider = () => {
  const [collapsed, setCollapsed] = useState(true);

  const { postLanguage } = useLanguages();
  const { postCourse } = useCourses();
  const { postArticle } = useArticles();

  const { getDecodedToken } = useAuth();

  const decodedToken = getDecodedToken();
  const userId = decodedToken?.sub;

  const { userLanguages, addLanguage } = useUserLanguages({
    authorId: userId,
  });
  const { userCourses, addCourse } = useUserCourses({ authorId: userId });
  const { userArticles, addArticle } = useUserArticles({ authorId: userId });

  const navigate = useNavigate();

  useEffect(() => {
    const handleKeyDown = (event: KeyboardEvent) => {
      if (
        event.target instanceof HTMLInputElement ||
        event.target instanceof HTMLTextAreaElement
      )
        return;

      if (event.code === "Backquote") {
        setCollapsed((prev) => !prev);
      }
    };

    document.addEventListener("keydown", handleKeyDown);

    return () => {
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, [collapsed]);

  async function onAddLanguageClick() {
    if (!userId) return;

    const lang = await postLanguage(
      LanguageUtils.defaultCreateLanguageModel(userId)
    );
    addLanguage(lang);

    navigate(RouteUtils.editLanguage(lang.id));
  }

  async function onAddCourseClick() {
    if (!userId) return;

    const course = await postCourse(
      CourseUtils.defaultCreateCourseModel(userId)
    );
    addCourse(course);

    navigate(RouteUtils.manageCourse(course.id));
  }

  async function onAddArticleClick() {
    if (!userId) return;

    const article = await postArticle(
      ArticleUtils.defaultCreateArticleModel(userId)
    );
    addArticle(article);

    navigate(RouteUtils.manageArticle(article.id));
  }

  const items: MenuItem[] = [
    createItem(
      <Link to={RouteUtils.profile()}>Profile</Link>,
      "a0",
      <UserOutlined />
    ),
    createItem("Languages", "sub1", <TranslationOutlined />, [
      createItem(
        <div onClick={onAddLanguageClick}>
          <PlusCircleOutlined /> Add Language
        </div>,
        "a1"
      ),
      ...userLanguages.map((lang, index) =>
        createItem(
          <Link to={RouteUtils.editLanguage(lang.id)}>{lang.name}</Link>,
          "a" + (index + 3).toString()
        )
      ),
    ]),
    createItem("Courses", "sub2", <BookOutlined />, [
      createItem(
        <div onClick={onAddCourseClick}>
          <PlusCircleOutlined /> Add Course
        </div>,
        "b0"
      ),
      ...userCourses.map((course, index) =>
        createItem(
          <Link to={RouteUtils.manageCourse(course.id)}>{course.title}</Link>,
          "b" + (index + 1).toString()
        )
      ),
    ]),
    createItem("Articles", "sub3", <ReadOutlined />, [
      createItem(
        <div onClick={onAddArticleClick}>
          <PlusCircleOutlined /> Add Article
        </div>,
        "c0"
      ),
      ...userArticles.map((article, index) =>
        createItem(
          <Link to={RouteUtils.manageArticle(article.id)}>
            {article.title}
          </Link>,
          "c" + (index + 1).toString()
        )
      ),
    ]),
  ];

  return (
    <Sider
      theme="light"
      collapsible
      collapsed={collapsed}
      onCollapse={(value) => setCollapsed(value)}
      style={{
        overflow: "auto",
        height: "100vh",
        position: "fixed",
        left: 0,
        top: 0,
        bottom: 0,
        zIndex: 10,
      }}
    >
      <Menu
        defaultSelectedKeys={["1"]}
        mode="inline"
        items={items}
        style={{
          paddingBottom: "3em",
        }}
      />
    </Sider>
  );
};

export default AppSider;
