import { JwtPayload, jwtDecode } from "jwt-decode";
import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import AuthUtils from "./auth-utils";

// Todo: in the current implementation logic of geting / setting token is inconsistent
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
  const [token, setTokenCore] = useState<string | null>(AuthUtils.getToken());

  const setToken = (newToken: string | null) => {
    AuthUtils.setToken(newToken);
    setTokenCore(newToken);
  };

  const getDecodedToken = () => {
    if (!token) {
      return null;
    }

    const decodedToken = jwtDecode(token);

    return decodedToken;
  };

  useEffect(() => {
    AuthUtils.setToken(token);
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

export const useAuth = () => {
  return useContext(AuthContext);
};

export default AuthProvider;
