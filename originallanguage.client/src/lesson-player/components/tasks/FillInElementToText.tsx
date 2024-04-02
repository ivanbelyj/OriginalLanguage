import React, { useEffect } from "react";
import { ITaskProps } from "../../models/ITaskProps";

const FillInElementToText: React.FC<ITaskProps> = ({
  task,
  setCurrentAnswer,
}) => {
  useEffect(() => {
    setCurrentAnswer("Todo");
  }, [task]);

  return <div>Todo: FillInElementToText</div>;
};

export default FillInElementToText;
