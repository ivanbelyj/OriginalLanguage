import { ITaskProps } from "../models/ITaskProps";
import { TaskType } from "../models/TaskType";
import ComposeElementsTask from "./tasks/ComposeElementsTask";
import EnterTextTask from "./tasks/EnterTextTask";
import FillInElementToText from "./tasks/FillInElementToText";
import { Typography } from "antd";

const { Title } = Typography;

const taskComponentMap = {
  [TaskType.ElementsToTranslation]: {
    component: ComposeElementsTask,
    title: "Compose a translation from the elements",
  },
  [TaskType.ElementsToText]: {
    component: ComposeElementsTask,
    title: "Compose a text from the elements",
  },
  [TaskType.FillInElementToText]: {
    component: FillInElementToText,
    title: "Fill in the element",
  },
  [TaskType.TextToTranslation]: {
    component: EnterTextTask,
    title: "Translate the text",
  },
  [TaskType.TranslationToText]: {
    component: EnterTextTask,
    title: "Translate the text",
  },
};

export const TaskRenderer: React.FC<ITaskProps> = ({
  task,
  currentAnswer,
  setCurrentAnswer,
}) => {
  const { component: TaskComponent, title } = taskComponentMap[task.taskType];

  return TaskComponent ? (
    <div>
      <Title level={4}>{title}</Title>
      <TaskComponent
        task={task}
        currentAnswer={currentAnswer}
        setCurrentAnswer={setCurrentAnswer}
      />
    </div>
  ) : (
    <div>Unknown task type (</div>
  );
};
