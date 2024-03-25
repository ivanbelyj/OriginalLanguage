import { TaskType } from "./tasks/TaskType";

export interface ITask {
  lessonSampleId: string;
  question: string;
  taskType: TaskType;
}

export interface ITaskAnswer {
  task: ITask;
  answer: string;
}
