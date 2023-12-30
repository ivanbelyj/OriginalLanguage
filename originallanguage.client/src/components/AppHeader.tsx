import { Avatar, Button, Menu } from "antd";
import { useState } from "react";
import { Link } from "react-router-dom";

export default function AppHeader() {
  const menuItems = [
    { key: "1", label: <Link to="/">Main</Link> },
    { key: "2", label: <Link to="/about">About</Link> },
    { key: "3", label: <Link to="/courses">Courses</Link> },
    { key: "4", label: <Link to="/languages">Languages</Link> },
  ];
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "0 1.5em",
        backgroundColor: "#fff",
        color: "#ffffff",
      }}
    >
      <Menu
        mode="horizontal"
        style={{ backgroundColor: "#fff", flexGrow: 1 }}
        items={menuItems}
      ></Menu>
      {isLoggedIn ? (
        <Avatar style={{ backgroundColor: "#e11", marginLeft: "auto" }}>
          U
        </Avatar>
      ) : (
        <Button type="primary" style={{ marginLeft: "auto" }}>
          <Link to="/login">Login</Link>
        </Button>
      )}
    </div>
  );
}
