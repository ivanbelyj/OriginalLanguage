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
  const navigate = useNavigate();

  async function onAddLanguageClick() {
    const lang = await postLanguage({
      authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
      name: "New language",
      nativeName: "",
      isConlang: true,
    });

    navigate(`/edit-language/${lang.id}`);
  }

  const items: MenuItem[] = [
    createItem(<Link to="/profile">Profile</Link>, "1", <UserOutlined />),
    createItem("Languages", "sub1", <TranslationOutlined />, [
      createItem(
        <div onClick={onAddLanguageClick}>
          <PlusCircleOutlined /> Add Language
        </div>,
        "2"
      ),
      createItem("Lang 1", "3"),
      createItem("Lang 2", "4"),
      createItem("Lang 3", "5"),
    ]),
    createItem("Courses", "sub2", <BookOutlined />, [
      createItem(
        <Link to="/edit-course">
          <PlusCircleOutlined /> Add Course
        </Link>,
        "5b"
      ),
      createItem("Course 1", "6"),
      createItem("Course 2", "8"),
    ]),
    //   getItem("Files", "9", <BookOutlined />),
  ];

  return (
    <Sider
      theme="light"
      trigger={null}
      collapsible
      collapsed={collapsed}
      onCollapse={(value) => setCollapsed(value)}
      style={
        {
          // overflow: "auto",
          // position: "fixed",
          // maxHeight: "80vh",
          // zIndex: 1,
        }
      }
    >
      <Menu defaultSelectedKeys={["1"]} mode="inline" items={items} />
      <Button
        type="text"
        icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
        onClick={() => setCollapsed(!collapsed)}
        style={{
          fontSize: "16px",
          width: 64,
          height: 64,
        }}
      />
    </Sider>
  );
};

export default AppSider;
