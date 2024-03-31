import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useLessonTasks } from "../hooks/useLessonTasks";
import { LessonPlayer } from "../components/LessonPlayer";
import { ITask } from "../models/models";

const LessonPlayerPage: React.FC = () => {
  const { id: lessonId } = useParams();
  const [tasks, setTasks] = useState<ITask[] | null>();

  // Todo: handle when lessonId is falsy
  const { generateLessonTasks } = useLessonTasks();

  useEffect(() => {
    if (lessonId) {
      generateLessonTasks(lessonId).then((tasks: ITask[]) => {
        setTasks(tasks);
        console.log("lessonId", lessonId);
        console.log("generated tasks: ", tasks);
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
