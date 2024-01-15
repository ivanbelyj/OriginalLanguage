import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";

const Logout = () => {
  const { setToken } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    setToken(null);
    navigate("/", { replace: true });
  };

  handleLogout();

  return null;
};

export default Logout;
