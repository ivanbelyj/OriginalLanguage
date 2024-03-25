import React from "react";
import { List } from "antd";
import { Link } from "react-router-dom";
import ILesson from "../models/ILesson";

interface ICourseLessonsProps {
  lessons: ILesson[];
}

export const CourseLessons: React.FC<ICourseLessonsProps> = ({
  lessons,
}: ICourseLessonsProps) => {
  return (
    <List
      dataSource={lessons}
      renderItem={(lesson, index) => {
        return (
          <List.Item>
            <div>
              <Link to={`/lessons/${lesson.id}/player`}>
                Lesson {lesson.number}
              </Link>
            </div>
          </List.Item>
        );
      }}
    ></List>
  );
};
