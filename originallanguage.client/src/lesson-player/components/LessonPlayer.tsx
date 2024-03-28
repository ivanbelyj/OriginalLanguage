import React from "react";
import { ITask } from "../types/models";
import { Button, Alert, Typography } from "antd";

import "../lesson-player.css";
import { useNavigate } from "react-router-dom";
import { useTasksPlay } from "../hooks/useTasksPlay";
import { TaskRenderer } from "./TaskRenderer";

const { Paragraph } = Typography;

interface ILessonPlayerProps {
  lessonId: string;
  tasks: ITask[];
}

export const LessonPlayer: React.FC<ILessonPlayerProps> = ({
  lessonId,
  tasks,
}) => {
  const {
    currentTaskIndex,
    isAnswerCorrect,
    setIsAnswerCorrect,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    handleNextTask,
  } = useTasksPlay(lessonId, tasks);

  const navigate = useNavigate();

  const handleButtonClick = () => {
    if (isAnswerCorrect === null) {
      // Check answer
      handleCheckAnswer(currentAnswer);
    } else {
      // Move next
      handleNextTask();
      setCurrentAnswer("");
      setIsAnswerCorrect(null);
    }
  };

  return tasks.length === 0 ? (
    <div>
      <div>The lesson is not filled with content yet (</div>
      <Button
        type="primary"
        style={{ marginTop: 16 }}
        onClick={() => navigate(-1)}
      >
        Go back
      </Button>
    </div>
  ) : (
    <div>
      <TaskRenderer
        task={tasks[currentTaskIndex]}
        currentAnswer={currentAnswer}
        setCurrentAnswer={setCurrentAnswer}
      />
      {isAnswerCorrect !== null && (
        <Paragraph>
          <Alert
            message={isAnswerCorrect ? "Correct!" : "Incorrect."}
            type={isAnswerCorrect ? "success" : "error"}
            showIcon
          />
        </Paragraph>
      )}
      <Paragraph>
        <Button
          type="primary"
          style={{ marginTop: 16 }}
          onClick={handleButtonClick}
        >
          {isAnswerCorrect === null ? "Check" : "Next"}
        </Button>
      </Paragraph>
    </div>
  );
};
