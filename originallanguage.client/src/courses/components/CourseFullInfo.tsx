import { useEffect, useState } from "react";
import { Button, Descriptions, Typography } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import Chat from "../../chats/components/Chat";
import { ChatGroupUtils } from "../../chats/chat-group-utils";
import { PlayCircleOutlined } from "@ant-design/icons";
import { useCourses } from "../hooks/useCourses";
import ICourse from "../models/ICourse";
import RouteUtils from "../../common/routes/RouteUtils";

const { Title, Paragraph } = Typography;

export default function CourseFullInfo() {
  const { id: courseId } = useParams<{ id: string }>();
  const { getCourse } = useCourses();

  const [course, setCourse] = useState<ICourse | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    if (courseId) {
      getCourse(courseId).then((courseData: ICourse) => {
        setCourse(courseData);
      });
    }
  }, [courseId]);

  if (!course) {
    return <div>Loading...</div>;
  }

  return (
    <>
      <Title level={3}>{course.title}</Title>
      <Descriptions column={2}>
        <Descriptions.Item label="Author">{course.authorId}</Descriptions.Item>
        <Descriptions.Item label="Language">
          {course.languageId}
        </Descriptions.Item>
        <Descriptions.Item label="Created">
          {course.dateTimeAdded.toLocaleString()}
        </Descriptions.Item>
      </Descriptions>

      <Title level={4}>Course Description</Title>
      <Paragraph>
        <div dangerouslySetInnerHTML={{ __html: "Todo: some info" }} />
      </Paragraph>
      <Paragraph>
        <Button
          type="primary"
          size="middle"
          onClick={() => {
            navigate(RouteUtils.courseLessons(courseId));
          }}
        >
          <PlayCircleOutlined /> Learn the course
        </Button>
      </Paragraph>

      <Title level={4}>Course Chat</Title>
      <Chat groupId={ChatGroupUtils.getCourseGroupId(course.id)} />
    </>
  );
}
