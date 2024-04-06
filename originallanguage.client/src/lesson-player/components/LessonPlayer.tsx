import React from "react";

import "../lesson-player.css";
import { LessonPlayerState, useTasksPlay } from "../hooks/useTasksPlay";
import { TaskRenderer } from "./TaskRenderer";
import { EmptyLesson } from "./EmptyLesson";
import LessonCompleted from "./LessonCompleted";
import { PlayerControl } from "./PlayerControl";
import { CompletionBar } from "./CompletionBar";
import { ITask } from "../models/models";

interface ILessonPlayerProps {
  lessonId: string;
  tasks: ITask[];
}

export const LessonPlayer: React.FC<ILessonPlayerProps> = ({
  tasks,
  lessonId,
}) => {
  const {
    currentTask,
    currentAnswer,
    setCurrentAnswer,
    handleCheckAnswer,
    completionResult,
    checkAnswerResult,
    completedTasksCount,
    playerState,
    moveToNextTask,
    handleCompleteLesson,
  } = useTasksPlay(lessonId, tasks);

  const handlePerformAction = async () => {
    switch (playerState) {
      case LessonPlayerState.NoAnswer:
        break;
      case LessonPlayerState.AnswerGiven:
        await handleCheckAnswer(currentAnswer);
        break;
      case LessonPlayerState.AnswerChecked:
        await moveToNextTask();
        break;
      case LessonPlayerState.LessonFinished:
        await handleCompleteLesson();
        break;
    }
  };

  if (completionResult) {
    return <LessonCompleted result={completionResult} />;
  }

  const lessonBody =
    tasks.length === 0 ? (
      <EmptyLesson />
    ) : (
      <div>
        <CompletionBar
          tasksCount={tasks.length}
          completedTasksCount={completedTasksCount}
        />
        <TaskRenderer
          task={currentTask}
          currentAnswer={currentAnswer}
          setCurrentAnswer={setCurrentAnswer}
        />
        <PlayerControl
          checkAnswerResult={checkAnswerResult}
          onPerformAction={handlePerformAction}
          playerState={playerState}
        />
      </div>
    );

  return (
    <div>
      <div>{lessonBody}</div>
    </div>
  );
};
