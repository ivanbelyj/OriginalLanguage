export enum TaskType {
  ElementsToTranslation = "elementsToTranslation",
  ElementsToText = "elementsToText",
  FillInElementToText = "fillInElementToText",
  TextToTranslation = "textToTranslation",
  TranslationToText = "translationToText",
}

export interface ITaskAnswer {
  task: ITask;
  answer: string;
}

export interface ITask {
  lessonSampleId: string;
  taskType: TaskType;
  question?: string;
  given?: string;
  hint?: string;
  glosses?: string;
}
