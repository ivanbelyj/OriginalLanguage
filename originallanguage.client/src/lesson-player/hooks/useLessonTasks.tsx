import axios from "axios";
import { ITask, ITaskAnswer } from "../models/ITaskAnswer";
import { useState } from "react";

export interface ICheckAnswerResult {
  isCorrect: boolean;
  correctAnswer: ITaskAnswer;
}

export interface ILessonCompletionResult {
  isSucceeded: boolean;
}

export async function checkAnswer(
  answer: ITaskAnswer
): Promise<ICheckAnswerResult> {
  try {
    const response = await axios.post(
      `${import.meta.env.VITE_API_URL}lessons/check-task-answer`,
      answer
    );
    console.log("check answer response", response);
    return response.data;
  } catch (err) {
    console.error(err);
    throw err;
  }
}

export async function completeLesson(
  lessonId: string,
  answers: ITaskAnswer[]
): Promise<ILessonCompletionResult> {
  try {
    const response = await axios.post(
      `${import.meta.env.VITE_API_URL}lessons/${lessonId}/complete`,
      answers
    );
    return response.data;
  } catch (err) {
    console.error(err);
    throw err;
  }
}

export async function generateLessonTasks(lessonId: string): Promise<ITask[]> {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_API_URL}lessons/${lessonId}/generate-tasks`
    );
    console.log("generate tasks response", response);
    return response.data;
  } catch (error) {
    throw error;
  }
}

export function useLessonTasks() {
  const [tasks, setTasks] = useState<ITask[] | null>();

  return {
    tasks,
    setTasks,
  };
}
