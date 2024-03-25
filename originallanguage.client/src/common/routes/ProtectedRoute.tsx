import { Navigate, Outlet } from "react-router-dom";
import { useJwtToken } from "../../auth/AuthProvider";

export const ProtectedRoute = () => {
  const { token } = useJwtToken();

  if (!token) {
    return <Navigate to="/login" />;
  }
  return <Outlet />;
};
