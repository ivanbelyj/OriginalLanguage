import { useParams } from "react-router-dom";
import EditCourseLessons from "../components/edit/EditCourseLessons";
import EditCourse from "../components/edit/EditCourse";
import PopconfirmButton from "../../common/components/PopconfirmButton";
import { DeleteOutlined } from "@ant-design/icons";
import { Tabs, Typography } from "antd";
import CourseSettings from "../components/edit/CourseSettings";
const { Title } = Typography;

const EditCoursePage = () => {
  const { id: courseId } = useParams();
  const onDelete = async () => {};

  const tabsItems = [
    {
      label: "Lessons",
      key: "1",
      children: (
        <>
          <EditCourse />
          {<EditCourseLessons courseId={courseId!} />}
        </>
      ),
    },
    {
      label: "Settings",
      key: "2",
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
