import React from "react";

import { useCourses } from "../hooks/useCourses";
import { Card, List } from "antd";
import Title from "antd/es/typography/Title";
import ICourse from "../models/ICourse";
import { CourseCard } from "../components/CourseCard";
import LanguageFlag from "../../languages/components/LanguageFlag";

const CoursesPage: React.FC = () => {
  const { courses } = useCourses();
  console.log("courses", courses);

  return (
    <div>
      <Title level={2}>Courses</Title>
      <List
        itemLayout="vertical"
        // pagination={{ pageSize: 5 }}
        dataSource={courses}
        renderItem={(course: ICourse) => (
          <List.Item>
            <CourseCard course={course} />
          </List.Item>
        )}
      />
    </div>
  );
};

export default CoursesPage;
