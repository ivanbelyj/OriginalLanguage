import { useEffect, useState } from "react";
import axios from "axios";
import ILessonSample from "../models/ILessonSample";

export function useSamplesOfLesson(lessonId: string) {
  const [samplesOfLesson, setSamplesOfLesson] = useState<ILessonSample[]>([]);

  async function fetchLessonSamples() {
    try {
      const response = await axios.get(
        `${import.meta.env.VITE_API_URL}lessons/${lessonId}/lesson-samples`
      );
      setSamplesOfLesson(response.data);
    } catch (error) {
      console.error(
        "There has been a problem with lesson samples fetch:",
        error
      );
    }
  }

  useEffect(() => {
    fetchLessonSamples();
  }, [lessonId]);

  return {
    samplesOfLesson,
  };
}
