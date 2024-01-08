/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_URL: string;
  readonly VITE_IDENTITY_URL: string;
  readonly VITE_DEBUG_USER_ID: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
