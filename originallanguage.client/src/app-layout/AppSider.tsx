import { Button, Layout, Menu, MenuProps } from "antd";

import {
  BookOutlined,
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  TranslationOutlined,
  UserOutlined,
  StarOutlined,
  PlusCircleOutlined,
} from "@ant-design/icons";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLanguages } from "../hooks/languages";
import { useCourses } from "../hooks/courses";
import { useUserLanguages } from "../hooks/useUserLanguages";
import { useAuth } from "../auth/AuthProvider";
import { useUserCourses } from "../hooks/useUserCourses";

const { Header, Content, Sider } = Layout;

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
  const [collapsed, setCollapsed] = useState(false);
  //   const {
  //     token: { colorBgContainer, borderRadiusLG },
  //   } = theme.useToken();

  const { postLanguage } = useLanguages();
  const { postCourse } = useCourses();

  const { getDecodedToken } = useAuth();

  const decodedToken = getDecodedToken();
  // console.log("user id from token:", decodedToken);
  const userId = decodedToken?.sub;

  const { userLanguages, addLanguage } = useUserLanguages({
    authorId: userId,
  });

  const { userCourses, addCourse } = useUserCourses({ authorId: userId });

  const navigate = useNavigate();

  async function onAddLanguageClick() {
    const lang = await postLanguage({
      authorId: userId,
      name: "New Language",
      conlangData: {
        type: "notSpecified",
        origin: "notSpecified",
        articulation: "common",
        subjectiveComplexity: "notSpecified",
        developmentStatus: "notSpecified",
        settingOrigin: "notSpecified",
        notHumanoidSpeakers: false,
        furrySpeakers: false,
      },
    });

    addLanguage(lang);

    navigate(`/edit-language/${lang.id}`);
  }

  async function onAddCourseClick() {
    if (!userId) return;

    const course = await postCourse({
      authorId: userId,
      title: "New Course",
    });

    addCourse(course);

    navigate(`/edit-course/${course.id}`);
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
          <Link to={`edit-course/${course.id}`}>{course.title}</Link>,
          "b" + (index + 1).toString()
        )
      ),
    ]),
  ];

  return (
    <Sider
      theme="light"
      // trigger={null}
      collapsible
      collapsed={collapsed}
      onCollapse={(value) => setCollapsed(value)}
    >
      <Menu defaultSelectedKeys={["1"]} mode="inline" items={items} />
    </Sider>
  );
};

export default AppSider;
