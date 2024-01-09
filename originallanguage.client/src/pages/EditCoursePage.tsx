import { useEffect, useState } from "react";
import EditCourse from "../components/courses/EditCourse";
import EditLessons from "../components/courses/EditLessons";
import ILesson from "../models/ILesson";
import { useLessons } from "../hooks/lessons";
import { useParams } from "react-router-dom";

const EditCoursePage = () => {
  const { id: courseId } = useParams();
  if (!courseId) return <div>Todo: handle empty course id</div>;

  const { courseLessons } = useLessons(courseId);
  const [editedLessons, setEditedLessons] = useState<ILesson[]>(courseLessons);

  const onAddLesson = () => {
    console.log("Todo: Add lesson");
  };

  useEffect(() => {
    setEditedLessons(courseLessons);
  }, [courseLessons]);

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
