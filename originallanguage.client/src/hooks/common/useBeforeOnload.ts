import { useEffect, useState } from "react";

export const useBeforeUnload = (callback: () => void) => {
  const [hasRun, setHasRun] = useState(false);

  useEffect(() => {
    const handleBeforeUnload = (event: BeforeUnloadEvent) => {
      if (!hasRun) {
        event.preventDefault();
        callback();
        setHasRun(true);
      }
    };

    const handleUnload = () => {
      if (!hasRun) {
        callback();
        setHasRun(true);
      }
    };

    window.addEventListener("beforeunload", handleBeforeUnload);
    window.addEventListener("unload", handleUnload);

    return () => {
      window.removeEventListener("beforeunload", handleBeforeUnload);
      window.removeEventListener("unload", handleUnload);
    };
  }, [callback, hasRun]);
};
