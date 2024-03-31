import { useState } from "react";
import {
  ICheckAnswerResult,
  ILessonCompletionResult,
  useLessonTasks,
} from "./useLessonTasks";
import { ITask } from "../models/models";

export function useTasksPlay(lessonId: string, tasks: ITask[]) {
  const { checkAnswer, completeLesson } = useLessonTasks();
  const [currentTaskIndex, setCurrentTaskIndex] = useState(0);
  const [checkAnswerResult, setCheckAnswerResult] =
    useState<ICheckAnswerResult | null>(null);
  const [currentAnswer, setCurrentAnswer] = useState("");
  const [answers, setAnswers] = useState(
    tasks.map((task) => ({ task, answer: "" }))
  );

  const [completion, setCompletion] = useState<ILessonCompletionResult | null>(
    null
  );

  const handleCheckAnswer = async (answer: string) => {
    const result = await checkAnswer({ task: tasks[currentTaskIndex], answer });
    setCheckAnswerResult(result);
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
      completeLesson(lessonId, answers).then(
        (lessonCompletionResult: ILessonCompletionResult) => {
          console.log("lesson is completed", lessonCompletionResult);
          setCompletion(lessonCompletionResult);
        }
      );
    }
  };

  return {
    currentTaskIndex,
    checkAnswerResult,
    setCheckAnswerResult,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    handleNextTask,
    completion,
  };
}
