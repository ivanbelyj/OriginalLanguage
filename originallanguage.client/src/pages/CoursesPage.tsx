import React from "react";

import { useCourses } from "../hooks/courses";

import { Card, List } from "antd";
import Title from "antd/es/typography/Title";
import { EditCourse } from "../components/courses/EditCourse";
import { CourseCard } from "../components/courses/CourseCard";
import ICourse from "../models/ICourse";

const CoursesPage: React.FC = () => {
  const { courses, addCourse } = useCourses();

  function handleCreateCourse(newCourse: ICourse) {
    addCourse(newCourse);
  }

  return (
    <Card>
      <Title level={2}>Courses</Title>
      <Title level={3}>Create course</Title>
      <EditCourse onCreate={handleCreateCourse} />

      <Title level={3}>Courses</Title>
      <List
        itemLayout="vertical"
        pagination={{ pageSize: 5 }}
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
