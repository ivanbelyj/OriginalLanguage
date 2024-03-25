import { useEffect, useState } from "react";
import axios from "axios";
import ILessonSample from "../models/ILessonSample";

export interface ICreateLessonSample {
  minimalProgressLevel: number;
  mainSentenceVariantId?: string;
  lessonId: string;
}

export interface IUpdateLessonSample {
  minimalProgressLevel: number;
  mainSentenceVariantId?: string;
  lessonId: string;
}

export function useLessonSamples(lessonId: string) {
  const [samplesOfLesson, setSamplesOfLesson] = useState<ILessonSample[]>([]);

  async function postLessonSample(
    lessonSample: ICreateLessonSample
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
    updateLessonSample: IUpdateLessonSample
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

  // async function getLessonSample(id: string): Promise<ILessonSample> {
  //   const response = await axios.get<ILessonSample>(
  //     import.meta.env.VITE_API_URL + "lesson-samples/" + id
  //   );

  //   return response.data;
  // }

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
  }, []);

  return {
    samplesOfLesson,
    // getLessonSample,
    postLessonSample,
    updateLessonSample,
    deleteLessonSample,
  };
}
