import useLogout from "../hooks/useLogout";

const Logout = () => {
  const { logout } = useLogout();
  logout();

  return null;
};

export default Logout;
