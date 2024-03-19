import { useNavigate } from "react-router-dom";
import { useJwtToken } from "./AuthProvider";

const Logout = () => {
  const { setToken } = useJwtToken();
  const navigate = useNavigate();

  const handleLogout = () => {
    setToken(null);
    navigate("/", { replace: true });
  };

  handleLogout();

  return null;
};

export default Logout;
