import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import { generateLessonTasks, useLessonTasks } from "../hooks/useLessonTasks";
import { LessonPlayer } from "../components/LessonPlayer";
import { ITask } from "../models/models";

const LessonPlayerPage: React.FC = () => {
  const { id: lessonId } = useParams();

  const { tasks, setTasks } = useLessonTasks();

  useEffect(() => {
    if (lessonId) {
      generateLessonTasks(lessonId).then((tasks: ITask[]) => {
        setTasks(tasks);
      });
    }
  }, [lessonId]);

  return (
    <div>
      {tasks && lessonId ? (
        <LessonPlayer lessonId={lessonId} tasks={tasks} />
      ) : (
        <div>Loading...</div>
      )}
    </div>
  );
};

export default LessonPlayerPage;
