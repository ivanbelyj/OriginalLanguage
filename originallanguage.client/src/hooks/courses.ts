import { useEffect, useState } from "react";
import ICourse from "../models/ICourse";

export function useCourses() {
  const [courses, setCourses] = useState<ICourse[]>([]);

  function addCourse(course: ICourse) {
    setCourses((prev) => {
      return [...prev, course];
    });
  }

  async function fetchCourses() {
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
  }
  useEffect(() => {
    fetchCourses();
  }, []);

  return { courses, addCourse };
}
