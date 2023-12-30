import { ReactNode } from "react";

interface ICenterProps {
  children: ReactNode;
}

const RegisterPage: React.FC<ICenterProps> = ({ children }: ICenterProps) => {
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "100vh",
      }}
    >
      {children}
    </div>
  );
};

export default RegisterPage;
