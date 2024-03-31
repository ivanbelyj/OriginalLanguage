import axios from "axios";
import { ITask, ITaskAnswer } from "../models/models";

export interface ICheckAnswerResult {
  isCorrect: boolean;
  correctAnswer: ITaskAnswer;
}

export interface ILessonCompletionResult {
  isSucceeded: boolean;
}

axios.interceptors.request.use((req) => {
  console.log(req);
  return req;
});

export function useLessonTasks() {
  async function generateLessonTasks(lessonId: string): Promise<ITask[]> {
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
    console.log("complete lessons", lessonId, answers);
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
