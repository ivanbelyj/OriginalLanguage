import React from "react";
import { CourseCard } from "../components/courses/course-card";
import { useCourses } from "../hooks/courses";
import { CreateCourse } from "../components/courses/create-course";
import { ICourse } from "../models/ICourse";

const CoursesList: React.FC = () => {
  const { courses, addCourse } = useCourses();

  function handleCreateCourse(newCourse: ICourse) {
    addCourse(newCourse);
  }

  return (
    <div>
      <h1>Courses</h1>
      <h2>Create course</h2>
      <CreateCourse onCreate={handleCreateCourse} />
      <h2>Courses</h2>
      {courses.map((course) => {
        return <CourseCard course={course} key={course.id} />;
      })}
    </div>
  );
};

export default CoursesList;
