import React from "react";
import { Typography } from "antd";
import { ILessonCompletionResult } from "../hooks/useLessonTasks";
const { Title } = Typography;

interface ILessonCompletedProps {
  result: ILessonCompletionResult;
}

const LessonCompleted: React.FC<ILessonCompletedProps> = ({ result }) => {
  return (
    <div>
      <Title level={4}>Congratulations!</Title>
      <div>
        The lesson was {result.isSucceeded ? "successful" : "not successful"}
      </div>
    </div>
  );
};

export default LessonCompleted;
