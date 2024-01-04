import { Avatar, Button, Layout, Menu, Switch } from "antd";
import { Link } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";

const { Header } = Layout;

export default function AppHeader() {
  const { token } = useAuth();

  const links = [
    <Link to="/">Main</Link>,
    <Link to="/about">About</Link>,

    <Link to="/languages">Languages</Link>,
    <Link to="/courses">Courses</Link>,
  ];

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "0 1.5em",
        backgroundColor: "#fff",
        color: "#ffffff",

        position: "sticky",
        top: "0",
        zIndex: 1,
        width: "100%",
      }}
    >
      <Menu
        mode="horizontal"
        style={{ backgroundColor: "#fff", flexGrow: 1 }}
        items={links.map((item, index) => ({ key: index, label: item }))}
      ></Menu>
      {token ? (
        <Link to="profile/">
          <Avatar style={{ backgroundColor: "#e11", marginLeft: "auto" }}>
            U
          </Avatar>
        </Link>
      ) : (
        <Button type="primary" style={{ marginLeft: "auto" }}>
          <Link to="/login">Login</Link>
        </Button>
      )}
    </div>
  );
}
