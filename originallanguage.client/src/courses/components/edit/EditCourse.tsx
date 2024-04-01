import { Form, Input, Typography } from "antd";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { useAuth } from "../../../auth/AuthProvider";
import { useCourses } from "../../hooks/useCourses";

const { Title } = Typography;

export interface IEditCourseProps {
  saveCourse?: () => void;
}

export default function EditCourse({ saveCourse }: IEditCourseProps) {
  const [form] = Form.useForm();
  const courseTitle = Form.useWatch("title", form);
  const { id: courseId } = useParams();

  const { updateCourse, getCourse } = useCourses();

  const { getDecodedToken } = useAuth();

  const decodedToken = getDecodedToken();
  const userId = decodedToken?.sub;

  const handleBlur = async () => {
    if (courseId) {
      await updateCourse(courseId, {
        ...form.getFieldsValue(),
        authorId: userId,
      });
    }

    saveCourse?.();
  };

  useEffect(() => {
    if (courseId) {
      getCourse(courseId).then((course) => {
        form.setFieldsValue(course);
      });
    }
  }, [courseId]);

  return (
    <>
      <Form form={form}>
        <Title level={3}>{courseTitle}</Title>
        <Form.Item
          label="Course title"
          name="title"
          rules={[
            { required: true, message: "Please input the course title!" },
          ]}
        >
          <Input type="text" placeholder="Course title" onBlur={handleBlur} />
        </Form.Item>
      </Form>
    </>
  );
}
