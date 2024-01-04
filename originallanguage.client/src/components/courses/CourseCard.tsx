import ICourse from "../../models/ICourse";
import { Card, Typography } from "antd";

const { Text, Paragraph } = Typography;

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <Card>
      <Text strong>{course.title}</Text>
      <Paragraph>Author ID: {course.authorId}</Paragraph>
      <Paragraph>Language ID: {course.languageId}</Paragraph>
      <Paragraph>Added: {course.dateTimeAdded?.toString()}</Paragraph>
    </Card>
  );
};
