import { useEffect, useState } from "react";
import axios from "axios";
import ILesson from "../models/ILesson";

export interface ICreateLesson {
  theoryArticleId: string;
  number: number;
  courseId: string;
}

export interface IUpdateLesson {
  theoryArticleId: string;
  number: number;
  courseId: string;
}

export function useLessons(courseId: string) {
  const [courseLessons, setCourseLessons] = useState<ILesson[]>([]);

  async function postLesson(lesson: ICreateLesson): Promise<ILesson> {
    const response = await axios.post<ILesson>(
      import.meta.env.VITE_API_URL + "lessons",
      lesson
    );

    console.log("Lesson post response: ", response);
    setCourseLessons((prev) => {
      return [...prev, response.data];
    });

    return response.data;
  }

  async function updateLesson(
    id: string,
    updateLesson: IUpdateLesson
  ): Promise<void> {
    await axios.put<ILesson>(
      import.meta.env.VITE_API_URL + "lessons/" + id,
      updateLesson
    );

    setCourseLessons((prev) => {
      return prev.map((lesson) =>
        lesson.id === id ? { ...lesson, ...updateLesson } : lesson
      );
    });
  }

  async function deleteLesson(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "lessons/" + id);

    setCourseLessons((prev) => {
      return prev.filter((lesson) => lesson.id !== id);
    });
  }

  function fetchLessons() {
    fetch(import.meta.env.VITE_API_URL + `courses/${courseId}/lessons`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setCourseLessons(data))
      .catch((error) => {
        console.error("There has been a problem with lessons fetch:", error);
      });
  }

  async function getLesson(id: string): Promise<ILesson> {
    const response = await axios.get<ILesson>(
      import.meta.env.VITE_API_URL + "lessons/" + id
    );

    return response.data;
  }

  useEffect(() => {
    fetchLessons();
  }, []);

  return {
    courseLessons,

    getLesson,
    postLesson,
    updateLesson,
    deleteLesson,
  };
}
