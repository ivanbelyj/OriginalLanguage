import React from "react";
import { Avatar, List, Steps, Tooltip, Typography } from "antd";
import { Link } from "react-router-dom";
import ILesson from "../models/ILesson";
import CourseUtils from "../course-utils";

const { Title } = Typography;

interface ICourseLessonItemProps {
  lesson: ILesson;
}

export const CourseLessonItem: React.FC<ICourseLessonItemProps> = ({
  lesson,
}) => {
  return (
    <>
      <Tooltip title={lesson.description} placement="bottom">
        <Link to={`/lessons/${lesson.id}/player`}>
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <Avatar
              size={64}
              style={{
                borderStyle: "solid",
                borderColor: "#555",
                borderWidth: 4,
              }}
            />

            <Title level={5}>{CourseUtils.getLessonTitle(lesson)}</Title>
          </div>
        </Link>
      </Tooltip>
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
