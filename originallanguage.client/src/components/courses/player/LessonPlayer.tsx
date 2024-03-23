import React, { useState } from "react";
import { useLessonTasks } from "./hooks/useLessonTasks";
import { ITask, ITaskAnswer } from "./lesson-player-models";
import { Input, Button, Alert } from "antd";

interface ILessonPlayerProps {
  lessonId: string;
  tasks: ITask[];
}

const LessonPlayer: React.FC<ILessonPlayerProps> = ({
  lessonId,
  tasks,
}: ILessonPlayerProps) => {
  const { checkAnswer, completeLesson } = useLessonTasks(lessonId);
  const [currentTaskIndex, setCurrentTaskIndex] = useState<number>(0);
  const [isAnswerCorrect, setIsAnswerCorrect] = useState<boolean | null>(null);
  const [answers, setAnswers] = useState<ITaskAnswer[]>(
    tasks.map((task) => ({ task, answer: "" }))
  );

  const handleCheckAnswer = async (answer: string) => {
    const result = await checkAnswer({ task: tasks[currentTaskIndex], answer });
    setIsAnswerCorrect(result.isCorrect);
    setAnswers((prevAnswers) => {
      const updatedAnswers = [...prevAnswers];
      updatedAnswers[currentTaskIndex].answer = answer;
      return updatedAnswers;
    });
  };

  const handleNextTask = () => {
    if (currentTaskIndex < tasks.length - 1) {
      setCurrentTaskIndex(currentTaskIndex + 1);
    } else {
      console.log("complete lesson");
      completeLesson(lessonId, answers);
    }
  };

  console.log("task", tasks[currentTaskIndex]);

  return (
    <div style={{ padding: "1.5em", background: "#fff", minHeight: 360 }}>
      <div>{tasks[currentTaskIndex].sentence}</div>
      <Input.TextArea
        placeholder="Enter your answer here"
        onChange={(e) => handleCheckAnswer(e.target.value)}
      />
      {isAnswerCorrect !== null && (
        <Alert
          message={isAnswerCorrect ? "Correct!" : "Incorrect."}
          type={isAnswerCorrect ? "success" : "error"}
          showIcon
        />
      )}
      <Button type="primary" onClick={handleNextTask} style={{ marginTop: 16 }}>
        Next
      </Button>
    </div>
  );
};

export default LessonPlayer;
