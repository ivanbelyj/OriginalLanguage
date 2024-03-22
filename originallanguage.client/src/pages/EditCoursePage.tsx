import EditCourse from "../components/courses/EditCourse";
import EditCourseLessons from "../components/courses/EditCourseLessons";
import { useParams } from "react-router-dom";

const EditCoursePage = () => {
  const { id: courseId } = useParams();

  const saveCourse = () => {
    console.log("save course");
  };

  return (
    <>
      <EditCourse saveCourse={saveCourse} />
      {<EditCourseLessons courseId={courseId!} />}
    </>
  );
};

export default EditCoursePage;
