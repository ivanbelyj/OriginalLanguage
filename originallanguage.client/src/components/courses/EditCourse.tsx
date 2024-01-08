import { Form, Input, Button, Typography, message } from "antd";
import { useCourses } from "../../hooks/courses";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

const { Title } = Typography;

export default function EditCourse() {
  const { updateCourse, getCourse } = useCourses();
  const [form] = Form.useForm();
  const courseTitle = Form.useWatch("title", form);
  const { id: courseId } = useParams();
  const [messageApi, contextHolder] = message.useMessage();

  // const [course, setCourse] = useState<ICourse>();

  const handleFinish = async (values: any) => {
    if (courseId) {
      await updateCourse(courseId, {
        ...values,
        authorId: import.meta.env.VITE_DEBUG_USER_ID, // Todo: actual author
      });
      messageApi.open({
        type: "success",
        content: "Course is saved",
      });
    }
  };

  useEffect(() => {
    if (courseId) {
      getCourse(courseId).then((course) => {
        // setCourse(course);
        form.setFieldsValue(course);
      });
    }
  }, [courseId]);

  return (
    <>
      {contextHolder}
      <Form form={form} onFinish={handleFinish}>
        <Title level={3}>{courseTitle}</Title>
        <Form.Item
          label="Course title"
          name="title"
          rules={[
            { required: true, message: "Please input the course title!" },
          ]}
        >
          <Input type="text" placeholder="Course title" />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            Save
          </Button>
        </Form.Item>
      </Form>
    </>
  );
}
