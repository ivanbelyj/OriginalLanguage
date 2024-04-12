import { useState } from "react";

const useForceUpdate = () => {
  const [_, setFakeState] = useState(0);
  const forceUpdate = () => {
    setFakeState((prev) => prev + 1);
  };
  return { forceUpdate };
};

export default useForceUpdate;
