import { Link } from "react-router-dom";
import { Avatar, Card, Typography } from "antd";
import ICourse from "../models/ICourse";
import LanguageFlag from "../../languages/components/LanguageFlag";
import { UserOutlined } from "@ant-design/icons";

const { Meta } = Card;

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <Card
      title={
        <>
          {/* Todo: get language name and flag*/}
          <LanguageFlag />{" "}
          <Link to={"/courses/" + course.id}>
            {course.title ?? "Course title"}
          </Link>
        </>
      }
    >
      <Link to="/">
        <Meta
          avatar={
            <Avatar>
              <UserOutlined />
            </Avatar>
          }
          title="User name"
        />
      </Link>
    </Card>
  );
};
