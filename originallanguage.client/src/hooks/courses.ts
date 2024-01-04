import { useEffect, useState } from "react";
import ICourse from "../models/ICourse";
import axios from "axios";

export interface ICreateCourse {
  authorId: string;
  title: string;
}

export interface IUpdateCourse {
  authorId: string;
  title: string;
}

export function useCourses() {
  const [courses, setCourses] = useState<ICourse[]>([]);

  // function addCourse(course: ICourse) {
  //   setCourses((prev) => {
  //     return [...prev, course];
  //   });
  // }

  async function postCourse(course: ICreateCourse): Promise<ICourse> {
    const response = await axios.post<ICourse>(
      import.meta.env.VITE_API_URL + "courses",
      course
    );

    console.log("Course post response: ", response);
    // addCourse(response.data);
    setCourses((prev) => {
      return [...prev, response.data];
    });

    return response.data;
  }

  async function updateCourse(
    id: string,
    updateCourse: IUpdateCourse
  ): Promise<void> {
    await axios.put<ICourse>(
      import.meta.env.VITE_API_URL + "courses/" + id,
      updateCourse
    );

    setCourses((prev) => {
      return prev.map((course) =>
        course.id === id ? { ...course, ...updateCourse } : course
      );
    });
  }

  async function deleteCourse(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "courses/" + id);

    setCourses((prev) => {
      return prev.filter((course) => course.id !== id);
    });
  }

  function fetchCourses() {
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

  return { courses, postCourse, updateCourse, deleteCourse };
}
