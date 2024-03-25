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

  async function fetchCourses() {
    try {
      const response = await axios.get(
        import.meta.env.VITE_API_URL + "courses",
        {
          params: {
            limit: 100,
          },
        }
      );

      setCourses(
        response.data.map((x: ICourse) => ({
          ...x,
          dateTimeAdded: new Date(x.dateTimeAdded),
        }))
      );
    } catch (error) {
      console.error("There has been a problem with courses fetch:", error);
    }
  }

  async function getCourse(id: string): Promise<ICourse> {
    const response = await axios.get<ICourse>(
      import.meta.env.VITE_API_URL + "courses/" + id
    );

    return response.data;

    // return {
    //   ...response.data,
    //   dateTimeAdded: new Date(response.data.dateTimeAdded),
    //   // dateTimeUpdated: new Date(response.data.dateTimeUpdated),
    // };
  }

  useEffect(() => {
    fetchCourses();
  }, []);

  return { courses, getCourse, postCourse, updateCourse, deleteCourse };
}
