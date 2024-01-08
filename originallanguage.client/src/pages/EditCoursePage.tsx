import { useEffect, useState } from "react";
import EditCourse from "../components/courses/EditCourse";
import EditLessons from "../components/courses/EditLessons";
import ILesson from "../models/ILesson";
import { useLessons } from "../hooks/lessons";

const EditCoursePage = () => {
  const { lessons } = useLessons();
  const [editedLessons, setEditedLessons] = useState<ILesson[]>(lessons);

  const onAddLesson = () => {
    console.log("Todo: Add lesson");
  };

  useEffect(() => {
    setEditedLessons(lessons);
  }, [lessons]);

  return (
    <>
      <EditCourse />

      {editedLessons && (
        <EditLessons
          lessons={editedLessons}
          setLessons={setEditedLessons}
          onAddLesson={onAddLesson}
        />
      )}
    </>
  );
};

export default EditCoursePage;
