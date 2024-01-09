import { useEffect, useState } from "react";
import axios from "axios";
import ILessonSample from "../models/ILessonSample";

// Todo: rename other hooks?

// Todo: decomposite hooks?

export function useLessonSamples(lessonId: string) {
  const [samplesOfLesson, setSamplesOfLesson] = useState<ILessonSample[]>([]);

  async function postLessonSample(
    lessonSample: ILessonSample
  ): Promise<ILessonSample> {
    const response = await axios.post<ILessonSample>(
      import.meta.env.VITE_API_URL + "lesson-samples",
      lessonSample
    );

    setSamplesOfLesson((prev) => {
      return [...prev, response.data];
    });

    return response.data;
  }

  async function updateLessonSample(
    id: string,
    updateLessonSample: ILessonSample
  ): Promise<void> {
    await axios.put<ILessonSample>(
      import.meta.env.VITE_API_URL + "lesson-samples/" + id,
      updateLessonSample
    );

    setSamplesOfLesson((prev) => {
      return prev.map((lessonSample) =>
        lessonSample.id === id
          ? { ...lessonSample, ...updateLessonSample }
          : lessonSample
      );
    });
  }

  async function deleteLessonSample(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "lesson-samples/" + id);

    setSamplesOfLesson((prev) => {
      return prev.filter((lessonSample) => lessonSample.id !== id);
    });
  }

  function fetchLessonSamples() {
    fetch(import.meta.env.VITE_API_URL + `lessons/${lessonId}/lesson-samples`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setSamplesOfLesson(data))
      .catch((error) => {
        console.error(
          "There has been a problem with lesson samples fetch:",
          error
        );
      });
  }

  async function getLessonSample(id: string): Promise<ILessonSample> {
    const response = await axios.get<ILessonSample>(
      import.meta.env.VITE_API_URL + "lesson-samples/" + id
    );

    return response.data;
  }

  useEffect(() => {
    fetchLessonSamples();
  }, []);

  return {
    samplesOfLesson,
    getLessonSample,
    postLessonSample,
    updateLessonSample,
    deleteLessonSample,
  };
}
