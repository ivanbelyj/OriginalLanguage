import { TaskType } from "./TaskType";

export interface ITask {
  lessonSampleId: string;
  taskType: TaskType;
  question?: string;
  given?: string;
  hint?: string;
  glosses?: string;
}

export interface ITaskAnswer {
  task: ITask;
  answer: string;
}
