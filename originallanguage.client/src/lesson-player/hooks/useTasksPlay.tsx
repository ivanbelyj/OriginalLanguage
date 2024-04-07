import { useEffect, useState } from "react";
import {
  ICheckAnswerResult,
  ILessonCompletionResult,
  checkAnswer,
  completeLesson,
} from "./useLessonTasks";
import { ITask, ITaskAnswer } from "../models/models";

export enum LessonPlayerState {
  NoAnswer,
  AnswerGiven,
  AnswerChecked,
  LessonFinished,
}

export function useTasksPlay(lessonId: string, tasks: ITask[]) {
  const [currentTaskIndex, setCurrentTaskIndex] = useState(0);
  const [checkAnswerResult, setCheckAnswerResult] =
    useState<ICheckAnswerResult | null>(null);
  const [currentAnswer, setCurrentAnswer] = useState("");
  const [answers, setAnswers] = useState<ITaskAnswer[]>([]);
  const [completedTasksCount, setCompletedTasksCount] = useState(0);
  const [failedTasks, setFailedTasks] = useState<ITask[]>([]);
  const [completionResult, setCompletionResult] =
    useState<ILessonCompletionResult | null>(null);

  const [currentTask, setCurrentTask] = useState<ITask>(tasks[0]);

  const [playerState, setPlayerState] = useState<LessonPlayerState>(
    LessonPlayerState.NoAnswer
  );

  useEffect(() => {
    setPlayerState(
      currentAnswer ? LessonPlayerState.AnswerGiven : LessonPlayerState.NoAnswer
    );
  }, [currentAnswer]);

  /**
   * Should be called when the answer is finally given by the user.
   */
  const handleCheckAnswer = async (answer: string) => {
    if (playerState !== LessonPlayerState.AnswerGiven)
      throw new Error("Invalid operation");

    const currentAnswer = { task: currentTask, answer };
    const result = await checkAnswer(currentAnswer);

    if (result.isCorrect) {
      completeAnswer(currentAnswer);
    } else {
      failTask(currentTask);
    }
    setCheckAnswerResult(result);

    setPlayerState(
      isLastTask() && result.isCorrect
        ? LessonPlayerState.LessonFinished
        : LessonPlayerState.AnswerChecked
    );
  };

  const completeAnswer = (currentAnswer: ITaskAnswer) => {
    setAnswers((prevAnswers) => [...prevAnswers, currentAnswer]);
    setCompletedTasksCount((prev) => prev + 1);
  };

  const failTask = (currentTask: ITask) => {
    console.log("Failed tasks", [...failedTasks, currentTask]);
    setFailedTasks((prev) => [...prev, currentTask]);
  };

  const moveToNextTask = async () => {
    if (playerState !== LessonPlayerState.AnswerChecked) {
      throw new Error("Invalid operation");
    }

    if (currentTaskIndex < tasks.length - 1) {
      moveToNextFirstAttemptTask();
    } else if (failedTasks.length > 0) {
      moveToNextFailedTask();
    }

    setPlayerState(LessonPlayerState.NoAnswer);
  };

  const isLastTask = () =>
    (currentTaskIndex === tasks.length - 1 && failedTasks.length === 0) ||
    (currentTaskIndex >= tasks.length && failedTasks.length === 1);

  const moveToNextFirstAttemptTask = () => {
    const newIndex = currentTaskIndex + 1;
    setCurrentTaskIndex(newIndex);
    setCurrentTask(tasks[newIndex]);
  };

  const moveToNextFailedTask = () => {
    const failedTask = failedTasks.shift()!;
    setCurrentTask(failedTask);
  };

  const handleCompleteLesson = async () => {
    if (playerState !== LessonPlayerState.LessonFinished) {
      throw new Error("Invalid operation");
    }
    const lessonCompletionResult = await completeLesson(lessonId, answers);
    setCompletionResult(lessonCompletionResult);
  };

  return {
    currentTask,
    completedTasksCount,
    checkAnswerResult,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    moveToNextTask,
    handleCompleteLesson,
    completionResult,
    playerState,
  };
}
