import { Layout, Menu, MenuProps, Tooltip } from "antd";

import {
  BookOutlined,
  TranslationOutlined,
  UserOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLanguages } from "../../../languages/hooks/useLanguages";
import { useCourses } from "../../../courses/hooks/useCourses";
import { useUserLanguages } from "../../../user/hooks/useUserLanguages";
import { useJwtToken } from "../../../auth/AuthProvider";
import { useUserCourses } from "../../../user/hooks/useUserCourses";
import LanguageUtils from "../../../languages/language-utils";
import CourseUtils from "../../../courses/course-utils";

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

  const { getDecodedToken } = useJwtToken();

  const decodedToken = getDecodedToken();
  const userId = decodedToken?.sub;

  const { userLanguages, addLanguage } = useUserLanguages({
    authorId: userId,
  });

  const { userCourses, addCourse } = useUserCourses({ authorId: userId });

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

    navigate(`/edit-language/${lang.id}`);
  }

  async function onAddCourseClick() {
    if (!userId) return;

    const course = await postCourse(
      CourseUtils.defaultCreateCourseModel(userId)
    );

    addCourse(course);

    navigate(`/manage-course/${course.id}`);
  }

  const items: MenuItem[] = [
    createItem(<Link to="/profile">Profile</Link>, "a0", <UserOutlined />),
    createItem("Languages", "sub1", <TranslationOutlined />, [
      createItem(
        <div onClick={onAddLanguageClick}>
          <PlusCircleOutlined /> Add Language
        </div>,
        "a1"
      ),
      ...userLanguages.map((lang, index) =>
        createItem(
          <Link to={`edit-language/${lang.id}`}>{lang.name}</Link>,
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
          <Link to={`manage-course/${course.id}`}>{course.title}</Link>,
          "b" + (index + 1).toString()
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
