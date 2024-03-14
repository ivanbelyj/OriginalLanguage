import { useEffect, useState } from "react";

import ISentence from "../models/ISentence";
import axios from "axios";

export function useLessonSampleSentences(lessonSampleId: string) {
  const [lessonSampleSentences, setLessonSampleSentences] = useState<
    ISentence[]
  >([]);

  async function fetchSentences() {
    try {
      const response = await axios.get(
        `${
          import.meta.env.VITE_API_URL
        }lesson-samples/${lessonSampleId}/sentences`
      );
      setLessonSampleSentences(response.data);
    } catch (error) {
      console.error("There has been a problem with sentences fetch:", error);
    }
  }

  useEffect(() => {
    fetchSentences();
  }, [lessonSampleId]);

  return {
    lessonSampleSentences,
  };
}
