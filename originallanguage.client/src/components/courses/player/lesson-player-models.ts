import { TaskType } from "./TaskType";

export interface ITask {
  lessonSampleId: string;
  sentence: string;
  taskType: TaskType;
}

export interface ITaskAnswer {
  task: ITask;
  answer: string;
}
