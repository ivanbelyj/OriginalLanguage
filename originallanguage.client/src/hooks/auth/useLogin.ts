import axios from "axios";
import { useJwtToken } from "../../auth/AuthProvider";

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
    } catch (error) {
      console.error("Failed to authenticate", error);
    }
  };

  return { login };
}
