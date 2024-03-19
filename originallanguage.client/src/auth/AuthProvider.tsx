import axios from "axios";
import { JwtPayload, jwtDecode } from "jwt-decode";
import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";

interface AuthContextType {
  token: string | null;
  setToken: (newToken: string | null) => void;
  getDecodedToken: () => JwtPayload | null;
}

const printUsingDefaultContextError = () => {
  console.error("Using default auth context");
};

const AuthContext = createContext<AuthContextType>({
  token: null,
  setToken: printUsingDefaultContextError,
  getDecodedToken: () => {
    printUsingDefaultContextError();
    return null;
  },
});

const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [token, setToken_] = useState<string | null>(
    localStorage.getItem("token") // Todo: don't use local storage?
  );
  const setToken = (newToken: string | null) => {
    setToken_(newToken);
    if (newToken === null) localStorage.removeItem("token");
    else localStorage.setItem("token", newToken);
  };

  const getDecodedToken = () => {
    if (!token) {
      return null;
    }

    const decodedToken = jwtDecode(token);

    return decodedToken;
  };

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
      localStorage.setItem("token", token);
    } else {
      delete axios.defaults.headers.common["Authorization"];
      localStorage.removeItem("token");
    }
  }, [token]);

  const contextValue = useMemo(
    () => ({
      token,
      setToken,
      getDecodedToken,
    }),
    [token]
  );

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export const useJwtToken = () => {
  return useContext(AuthContext);
};

export default AuthProvider;
