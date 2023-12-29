import React, { useEffect, useState } from "react";
import { ICourse } from "../models/ICourse";
import { CourseCard } from "../components/course-card";

const CoursesList: React.FC = () => {
  const [courses, setCourses] = useState<ICourse[]>([]);

  useEffect(() => {
    fetch(import.meta.env.VITE_API_URL + "courses")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setCourses(data))
      .catch((error) => {
        console.error("There has been a problem with courses fetch:", error);
      });
  }, []);

  return (
    <div>
      <h1>Courses</h1>
      {courses.map((course) => {
        return <CourseCard course={course} key={course.id} />;
      })}
    </div>
  );
};

export default CoursesList;
