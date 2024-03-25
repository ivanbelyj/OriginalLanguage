import { useParams } from "react-router-dom";
import EditCourseLessons from "../components/edit/EditCourseLessons";
import EditCourse from "../components/edit/EditCourse";

const EditCoursePage = () => {
  const { id: courseId } = useParams();

  return (
    <>
      <EditCourse />
      {<EditCourseLessons courseId={courseId!} />}
    </>
  );
};

export default EditCoursePage;
