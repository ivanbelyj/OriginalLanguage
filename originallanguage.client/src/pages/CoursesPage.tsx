import React from "react";

import { useCourses } from "../hooks/courses";
import { Card, List } from "antd";
import Title from "antd/es/typography/Title";
import { CourseCard } from "../components/courses/CourseCard";

const CoursesPage: React.FC = () => {
  const { courses } = useCourses();

  return (
    <Card>
      <Title level={2}>Courses</Title>
      <List
        itemLayout="vertical"
        // pagination={{ pageSize: 5 }}
        dataSource={courses}
        renderItem={(course) => (
          <List.Item>
            <CourseCard course={course} />
          </List.Item>
        )}
      />
    </Card>
  );
};

export default CoursesPage;
