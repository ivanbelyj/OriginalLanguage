import { useNavigate } from "react-router-dom";
import { useJwtToken } from "../AuthProvider";

const useLogout = () => {
  const { setToken } = useJwtToken();
  const navigate = useNavigate();

  const logout = () => {
    setToken(null);
    navigate("/", { replace: true });
  };

  return { logout };
};

export default useLogout;
