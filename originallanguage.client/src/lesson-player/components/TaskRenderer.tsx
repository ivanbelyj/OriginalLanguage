import { ITaskProps } from "../models/ITaskProps";
import { TaskType } from "../models/TaskType";
import ComposeElementsTask from "./tasks/ComposeElementsTask";
import EnterTextTask from "./tasks/EnterTextTask";
import FillInElementToText from "./tasks/FillInElementToText";

const taskComponentMap = {
  [TaskType.ElementsToTranslation]: ComposeElementsTask,
  [TaskType.ElementsToText]: ComposeElementsTask,
  [TaskType.FillInElementToText]: FillInElementToText,
  [TaskType.TextToTranslation]: EnterTextTask,
  [TaskType.TranslationToText]: EnterTextTask,
};

export const TaskRenderer: React.FC<ITaskProps> = ({
  task,
  currentAnswer,
  setCurrentAnswer,
}) => {
  const TaskComponent = taskComponentMap[task.taskType];
  return TaskComponent ? (
    <TaskComponent
      task={task}
      currentAnswer={currentAnswer}
      setCurrentAnswer={setCurrentAnswer}
    />
  ) : (
    <div>Unknown task type (</div>
  );
};
