import { useJwtToken } from "../AuthProvider";
import AuthUtils from "../auth-utils";
import { useNavigate } from "react-router-dom";

export function useLoginLogout() {
  const { setToken } = useJwtToken();
  const navigate = useNavigate();

  const login = async (username: string, password: string) => {
    try {
      const response = await AuthUtils.fetchTokens(username, password);
      setToken(AuthUtils.getToken());
      return createResult(true, response);
    } catch (error: any) {
      console.error("Failed to authenticate", error);
      return createResult(false, error.response);
    }
  };

  const createResult = (isSucceeded: boolean, data: any) => {
    return { isSucceeded, data };
  };

  const logout = () => {
    setToken(null);
    navigate("/", { replace: true });
  };

  return { login, logout };
}
