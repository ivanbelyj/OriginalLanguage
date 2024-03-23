import React from "react";
import ILesson from "../../models/ILesson";
import { List } from "antd";
import { Link } from "react-router-dom";

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
