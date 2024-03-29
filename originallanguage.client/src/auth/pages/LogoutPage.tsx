import { useLoginLogout } from "../hooks/useLoginLogout";

const Logout = () => {
  const { logout } = useLoginLogout();
  logout();

  return null;
};

export default Logout;
