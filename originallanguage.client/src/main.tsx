import ReactDOM from "react-dom/client";

import AuthProvider from "./auth/AuthProvider.tsx";
import App from "./App.tsx";
import { SignalRProvider } from "./chats/SignalRContext.tsx";

import "antd/dist/reset.css";
import AuthUtils from "./auth/auth-utils.ts";
import React from "react";

AuthUtils.updateAxiosDefaults();

ReactDOM.createRoot(document.getElementById("root")!).render(
  // <React.StrictMode>
  <AuthProvider>
    <SignalRProvider>
      <App />
    </SignalRProvider>
  </AuthProvider>
  //</React.StrictMode>
);
