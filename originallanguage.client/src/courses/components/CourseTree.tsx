import React from "react";
import { List } from "antd";
import { CourseLessonItem } from "./CourseLessonItem"; // Импортируйте новый компонент
import ILesson from "../models/ILesson";
import _ from "lodash";

import "../styles/course-tree.css";

interface ICourseLessonsProps {
  lessons: ILesson[];
}

export const CourseTree: React.FC<ICourseLessonsProps> = ({ lessons }) => {
  const getLessonsRows = (): ILesson[][] => {
    let rows: ILesson[][] = [];
    let currentRow: ILesson[] = [];

    lessons.forEach((lesson, index) => {
      currentRow.push(lesson);

      const randomNumber = Math.floor(Math.random() * 3) + 1;

      if (
        index % randomNumber === randomNumber - 1 ||
        index === lessons.length - 1
      ) {
        rows.push(currentRow);
        currentRow = [];
      }
    });

    return rows;
  };

  return (
    <div className="course-tree">
      {getLessonsRows().map((row, rowIndex) => (
        <div className="course-tree__row" key={rowIndex}>
          {row.map((lesson, index) => (
            <div className="course-tree__item" key={index}>
              <CourseLessonItem lesson={lesson} />
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};
