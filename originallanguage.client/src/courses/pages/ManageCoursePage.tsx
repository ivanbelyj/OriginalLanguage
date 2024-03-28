import { useNavigate, useParams } from "react-router-dom";
import EditCourseLessons from "../components/edit/EditCourseLessons";
import EditCourse from "../components/edit/EditCourse";
import { Tabs } from "antd";
import CourseSettings from "../components/edit/CourseSettings";
import { CourseTree } from "../components/CourseTree";
import { useLessons } from "../hooks/useLessons";

const ManageCoursePage = () => {
  const { id: courseId, activeTab } = useParams();

  const { courseLessons, postLesson, updateLessonNumbers, deleteLesson } =
    useLessons(courseId!);

  const navigate = useNavigate();

  const tabItems = [
    {
      label: "Lessons",
      key: "lessons",
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
      key: "preview",
      children: <CourseTree lessons={courseLessons} />,
    },
    {
      label: "Settings",
      key: "settings",
      children: <CourseSettings courseId={courseId!} />,
    },
  ];

  const navigateToTab = (newTabKey: string) => {
    navigate(`/manage-course/${courseId}/${newTabKey}`);
  };

  const isActiveTabCorrect = () => {
    return activeTab && tabItems.find((x) => x.key === activeTab);
  };

  return (
    <>
      <Tabs
        items={tabItems}
        activeKey={isActiveTabCorrect() ? activeTab : tabItems.at(0)!.key}
        onChange={navigateToTab}
      ></Tabs>
    </>
  );
};

export default ManageCoursePage;
