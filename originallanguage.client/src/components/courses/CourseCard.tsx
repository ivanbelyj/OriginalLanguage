import { Link } from "react-router-dom";
import ICourse from "../../models/ICourse";
import { Avatar, Card, Typography } from "antd";

const { Text, Paragraph } = Typography;
const { Meta } = Card;

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <Card
      title={
        <>
          {/* Todo: get language name*/}
          <Link to={"/courses/" + course.id}>
            {course.title ?? "Course title"}
          </Link>
        </>
      }
    >
      <Paragraph>Added: {course.dateTimeAdded?.toLocaleString()}</Paragraph>
      <Meta
        avatar={<Avatar>U</Avatar>}
        title="User name"
        description="Some description"
      />
    </Card>
  );
};
