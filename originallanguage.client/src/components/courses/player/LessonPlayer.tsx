import React, { useState } from "react";
import { useLessonTasks } from "./hooks/useLessonTasks";
import { ITask, ITaskAnswer } from "./lesson-player-models";
import { Input, Button, Alert, Typography } from "antd";

const { Paragraph } = Typography;

interface ILessonPlayerProps {
  lessonId: string;
  tasks: ITask[];
}

const LessonPlayer: React.FC<ILessonPlayerProps> = ({
  lessonId,
  tasks,
}: ILessonPlayerProps) => {
  const { checkAnswer, completeLesson } = useLessonTasks();
  const [currentTaskIndex, setCurrentTaskIndex] = useState<number>(0);
  const [isAnswerCorrect, setIsAnswerCorrect] = useState<boolean | null>(null);
  const [currentAnswer, setCurrentAnswer] = useState<string>("");
  const [answers, setAnswers] = useState<ITaskAnswer[]>(
    tasks.map((task) => ({ task, answer: "" }))
  );

  const handleCheckAnswer = async (answer: string) => {
    const result = await checkAnswer({ task: tasks[currentTaskIndex], answer });
    console.log("check answer result", result);
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

  const handleNextButtonClick = (e: React.MouseEvent<HTMLInputElement>) => {
    handleCheckAnswer(currentAnswer).then(() => {
      handleNextTask();
      setCurrentAnswer("");
    });
  };

  console.log("task", tasks[currentTaskIndex]);

  return (
    <div>
      <Paragraph>
        <div>{tasks[currentTaskIndex].question}</div>
      </Paragraph>
      <Paragraph>
        <Input.TextArea
          placeholder="Enter your answer here"
          value={currentAnswer}
          onChange={(e) => {
            setCurrentAnswer(e.target.value);
          }}
        />
      </Paragraph>

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
          onClick={handleNextButtonClick}
        >
          Next
        </Button>
      </Paragraph>
    </div>
  );
};

export default LessonPlayer;
