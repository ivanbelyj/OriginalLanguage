import ICourse from "../../models/ICourse";
import { Avatar, Card, Typography } from "antd";

const { Text, Paragraph } = Typography;
const { Meta } = Card;

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <Card title={course.title}>
      <Paragraph>Added: {course.dateTimeAdded?.toLocaleString()}</Paragraph>
      <Meta
        avatar={<Avatar>U</Avatar>}
        title="User name"
        description="Some description"
      />
    </Card>
  );
};
