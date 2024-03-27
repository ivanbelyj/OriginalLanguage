import React from "react";
import { Avatar, List, Steps, Typography } from "antd";
import { Link } from "react-router-dom";
import ILesson from "../models/ILesson";

const { Title } = Typography;

interface ICourseLessonItemProps {
  lesson: ILesson;
}

export const CourseLessonItem: React.FC<ICourseLessonItemProps> = ({
  lesson,
}) => {
  return (
    <>
      <div style={{ display: "flex", flexDirection: "column" }}>
        <Link to={`/lessons/${lesson.id}/player`}>
          <Avatar
            size={64}
            style={{
              borderStyle: "solid",
              borderColor: "#555",
              borderWidth: 4,
            }}
          />

          <Title level={5}>Lesson {lesson.number}</Title>
        </Link>
      </div>
      {/* <Steps
        direction="vertical"
        size="small"
        current={1}
        items={[
          { title: "Finished", description: "sdfsdf", style: { height: 100 } },
          {
            title: "In Progress",
            description: "sdfsdf",
          },
          {
            title: "Waiting",
            description: "sdfsdf",
          },
        ]}
      /> */}
    </>
  );
};
