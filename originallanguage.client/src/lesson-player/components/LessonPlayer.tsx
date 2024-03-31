import React from "react";

import "../lesson-player.css";
import { useTasksPlay } from "../hooks/useTasksPlay";
import { TaskRenderer } from "./TaskRenderer";
import { ITask } from "../models/models";
import { EmptyLesson } from "./EmptyLesson";
import LessonCompleted from "./LessonCompleted";
import { PlayerControl } from "./PlayerControl";
import { CompletionBar } from "./CompletionBar";

interface ILessonPlayerProps {
  lessonId: string;
  tasks: ITask[];
}

export const LessonPlayer: React.FC<ILessonPlayerProps> = ({
  lessonId,
  tasks,
}) => {
  const {
    currentTaskIndex,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    handleNextTask,
    completion,
    checkAnswerResult,
    setCheckAnswerResult,
  } = useTasksPlay(lessonId, tasks);

  const handleButtonClick = () => {
    if (checkAnswerResult === null) {
      handleCheckAnswer(currentAnswer);
    } else {
      // Move next
      handleNextTask();
      setCurrentAnswer("");
      setCheckAnswerResult(null);
    }
  };

  if (completion) {
    return <LessonCompleted result={completion} />;
  }

  const lessonBody =
    tasks.length === 0 ? (
      <EmptyLesson />
    ) : (
      <div>
        <TaskRenderer
          task={tasks[currentTaskIndex]}
          currentAnswer={currentAnswer}
          setCurrentAnswer={setCurrentAnswer}
        />
        <PlayerControl
          checkAnswerResult={checkAnswerResult}
          onClick={handleButtonClick}
        />
      </div>
    );

  return (
    <div>
      <CompletionBar
        tasksCount={tasks.length}
        currentTaskIndex={currentTaskIndex}
      />
      <div>{lessonBody}</div>
    </div>
  );
};
