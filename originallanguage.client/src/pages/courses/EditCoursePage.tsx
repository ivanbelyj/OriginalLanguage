import EditCourse from "../../components/courses/edit/EditCourse";
import EditCourseLessons from "../../components/courses/edit/EditCourseLessons";
import { useParams } from "react-router-dom";

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
