import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../auth/AuthProvider";
import RouteUtils from "./RouteUtils";

export const ProtectedRoute = () => {
  const { token } = useAuth();

  if (!token) {
    return <Navigate to={RouteUtils.login()} />;
  }
  return <Outlet />;
};
