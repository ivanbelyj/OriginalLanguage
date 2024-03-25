import axios from "axios";
import { ITask, ITaskAnswer } from "../lesson-player-models";

export interface ICheckAnswerResult {
  isCorrect: boolean;
  correctAnswer: ITaskAnswer;
}

export interface ILessonCompletionResult {
  isSucceeded: boolean;
}

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

axios.interceptors.request.use((req) => {
  console.log(req);
  return req;
});

export function useLessonTasks() {
  async function checkAnswer(answer: ITaskAnswer): Promise<ICheckAnswerResult> {
    try {
      console.log("checking ", answer);
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

  async function completeLesson(
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

  return { generateLessonTasks, checkAnswer, completeLesson };
}
