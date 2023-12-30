import React from "react";

import { useCourses } from "../hooks/Courses";

import { ICourse } from "../models/ICourse";
import { Card, List } from "antd";
import Title from "antd/es/typography/Title";
import { CreateCourse } from "../components/courses/CreateCourse";
import { CourseCard } from "../components/courses/CourseCard";

const CoursesPage: React.FC = () => {
  const { courses, addCourse } = useCourses();

  function handleCreateCourse(newCourse: ICourse) {
    addCourse(newCourse);
  }

  return (
    <Card>
      <Title level={2}>Courses</Title>
      <Title level={3}>Create course</Title>
      <CreateCourse onCreate={handleCreateCourse} />

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
