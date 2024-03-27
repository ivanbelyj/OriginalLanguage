import { useParams } from "react-router-dom";
import EditCourseLessons from "../components/edit/EditCourseLessons";
import EditCourse from "../components/edit/EditCourse";
import { Tabs } from "antd";
import CourseSettings from "../components/edit/CourseSettings";
import { CourseTree } from "../components/CourseTree";
import { useLessons } from "../hooks/useLessons";

const EditCoursePage = () => {
  const { id: courseId } = useParams();

  const { courseLessons, postLesson, updateLessonNumbers, deleteLesson } =
    useLessons(courseId!);

  const tabsItems = [
    {
      label: "Lessons",
      key: "1",
      children: (
        <>
          <EditCourse />
          {
            <EditCourseLessons
              courseId={courseId!}
              courseLessons={courseLessons}
              postLesson={postLesson}
              updateLessonNumbers={updateLessonNumbers}
              deleteLesson={deleteLesson}
            />
          }
        </>
      ),
    },
    {
      label: "Preview",
      key: "2",
      children: <CourseTree lessons={courseLessons} />,
    },
    {
      label: "Settings",
      key: "3",
      children: <CourseSettings courseId={courseId!} />,
    },
  ];

  return (
    <>
      <Tabs items={tabsItems}></Tabs>
    </>
  );
};

export default EditCoursePage;
