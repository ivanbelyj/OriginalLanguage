import React from "react";
import ReactDOM from "react-dom/client";
// import "./index.css";

import "antd/dist/reset.css";
import AuthProvider from "./AuthProvider.tsx";
import App from "./App.tsx";
// import "antd/dist/antd.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <AuthProvider>
      <App />
    </AuthProvider>
  </React.StrictMode>
);
