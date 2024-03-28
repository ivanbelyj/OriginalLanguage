import { ITask } from "./models";

export interface ITaskProps {
  task: ITask;
  currentAnswer: string;
  setCurrentAnswer: (answer: string) => void;
}
