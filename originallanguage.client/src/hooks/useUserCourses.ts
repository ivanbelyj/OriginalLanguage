import { useEffect, useState } from "react";
import ICourse from "../models/ICourse";
import axios, { AxiosResponse } from "axios";

export function useUserCourses({ authorId }: { authorId?: string }) {
  const [userCourses, setUserCourses] = useState<ICourse[]>([]);

  function addCourse(newCourse: ICourse) {
    setUserCourses([...userCourses, newCourse]);
  }

  function removeCourse(courseToRemove: ICourse) {
    setUserCourses(
      userCourses.filter((course) => course.id !== courseToRemove.id)
    );
  }

  async function fetchCourses() {
    if (!authorId) return;

    const url = import.meta.env.VITE_API_URL + `accounts/${authorId}/courses`;

    console.log("fetch courses", url);

    try {
      const response: AxiosResponse<ICourse[]> = await axios.get<ICourse[]>(
        url
      );

      if (!(response.status === 200)) {
        throw new Error("Network response was not ok");
      }
      setUserCourses(response.data);
    } catch (error: any) {
      console.error("There has been a problem with user courses fetch:", error);
    }
  }

  useEffect(() => {
    fetchCourses();
  }, [authorId]);

  return { userCourses, addCourse, removeCourse };
}
