import axios from "axios";
import { ITask, ITaskAnswer } from "../lesson-player-models";

export interface ICheckAnswerResponse {
  isCorrect: boolean;
}

export interface ICompleteLessonResponse {}

async function generateLessonTasks(lessonId: string): Promise<ITask[]> {
  try {
    const response = await axios.get(
      `${import.meta.env.VITE_API_URL}lessons/${lessonId}/generate-tasks`
    );
    return response.data;
  } catch (error) {
    console.error(error);
    throw error;
  }
}

export function useLessonTasks(lessonId: string) {
  async function checkAnswer(
    answer: ITaskAnswer
  ): Promise<ICheckAnswerResponse> {
    try {
      const response = await axios.post(
        `${import.meta.env.VITE_API_URL}lessons/${lessonId}/check-answer`,
        answer
      );
      return response.data;
    } catch (err) {
      console.error(err);
      throw err;
    }
  }

  async function completeLesson(
    lessonId: string,
    answers: ITaskAnswer[]
  ): Promise<ICompleteLessonResponse> {
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

  return { generateLessonTasks, checkAnswer, completeLesson };
}
