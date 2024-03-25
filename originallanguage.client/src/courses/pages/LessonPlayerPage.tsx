import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ITask } from "../../lesson-player/models/models";
import { useLessonTasks } from "../../lesson-player/hooks/useLessonTasks";
import LessonPlayer from "../../lesson-player/components/LessonPlayer";

const LessonPlayerPage: React.FC = () => {
  const { id: lessonId } = useParams();
  const [tasks, setTasks] = useState<ITask[] | null>();

  // Todo: handle when lessonId is falsy
  const { generateLessonTasks } = useLessonTasks(lessonId!);

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
