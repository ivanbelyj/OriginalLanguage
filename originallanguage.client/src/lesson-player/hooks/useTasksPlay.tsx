import { useState } from "react";
import { ITask } from "../types/models";
import { useLessonTasks } from "./useLessonTasks";

export function useTasksPlay(lessonId: string, tasks: ITask[]) {
  const { checkAnswer, completeLesson } = useLessonTasks();
  const [currentTaskIndex, setCurrentTaskIndex] = useState(0);
  const [isAnswerCorrect, setIsAnswerCorrect] = useState<boolean | null>(null);
  const [currentAnswer, setCurrentAnswer] = useState("");
  const [answers, setAnswers] = useState(
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
      completeLesson(lessonId, answers).then((lessonCompletionResult) => {
        console.log(lessonCompletionResult);
      });
    }
  };

  return {
    currentTaskIndex,
    isAnswerCorrect,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    handleNextTask,
    setIsAnswerCorrect,
  };
}
