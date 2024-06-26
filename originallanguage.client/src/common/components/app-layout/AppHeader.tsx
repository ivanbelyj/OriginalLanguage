import { Avatar, Button, Menu } from "antd";
import { Link, useLocation } from "react-router-dom";
import { useAuth } from "../../../auth/AuthProvider";
import RouteUtils from "../../routes/RouteUtils";

const mainLinkKey = "main";

export default function AppHeader() {
  const { token } = useAuth();
  const location = useLocation();

  const items = [
    { key: mainLinkKey, label: <Link to={RouteUtils.main()}>Main</Link> },
    { key: "about", label: <Link to={RouteUtils.about()}>About</Link> },
    {
      key: "languages",
      label: <Link to={RouteUtils.languages()}>Languages</Link>,
    },
    { key: "courses", label: <Link to={RouteUtils.courses()}>Courses</Link> },
  ];

  const getCurrentItemKeys = () => {
    let locationName = location.pathname.substring(1);
    if (locationName === "") locationName = mainLinkKey;

    const currentItem = items.find((x) => x.key === locationName);
    return currentItem ? [currentItem.key] : [];
  };

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
        items={items}
        selectedKeys={getCurrentItemKeys()}
        // items={links.map((item, index) => ({ key: index, label: item }))}
      ></Menu>
      {token ? (
        <Link to={RouteUtils.profile()}>
          <Avatar style={{ backgroundColor: "#e11", marginLeft: "auto" }}>
            U
          </Avatar>
        </Link>
      ) : (
        <Button type="primary" style={{ marginLeft: "auto" }}>
          <Link to={RouteUtils.login()}>Login</Link>
        </Button>
      )}
    </div>
  );
}
