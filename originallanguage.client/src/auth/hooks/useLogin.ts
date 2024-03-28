import axios from "axios";
import { useJwtToken } from "../AuthProvider";

export function useLogin() {
  const { setToken } = useJwtToken();

  const login = async (username: string, password: string) => {
    try {
      const response = await axios.post(
        import.meta.env.VITE_IDENTITY_URL + "connect/token",
        {
          client_id: "frontend",
          client_secret: "secret",
          grant_type: "password",
          username,
          password: password,
          scope: "offline_access content_write courses_learn",
          // scope: "openid profile",
        },
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );
      console.log(response.data);
      setToken(response.data.access_token);
      return createResult(true, response.data);
    } catch (error: any) {
      console.error("Failed to authenticate", error);
      return createResult(false, error.response.data);
    }
  };

  const createResult = (isSucceeded: boolean, data: any) => {
    return { isSucceeded, data };
  };

  return { login };
}
