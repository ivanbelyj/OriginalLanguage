import { useEffect, useState } from "react";
import axios from "axios";
import ISentence from "../models/ISentence";

export interface ICreateSentence {
  lessonSampleId: string;

  text?: string;
  translation?: string;
  literalTranslation?: string;
  glosses?: string;
  transcription?: string;
}

export interface IUpdateSentence {
  lessonSampleId: string;

  text?: string;
  translation?: string;
  literalTranslation?: string;
  glosses?: string;
  transcription?: string;
}

export function useSentences(lessonSampleId: string) {
  const [lessonSampleSentences, setLessonSampleSentences] = useState<
    ISentence[]
  >([]);

  async function postSentence(sentence: ICreateSentence): Promise<ISentence> {
    const response = await axios.post<ISentence>(
      import.meta.env.VITE_API_URL + "sentences",
      sentence
    );

    setLessonSampleSentences((prev) => {
      return [...prev, response.data];
    });

    return response.data;
  }

  async function updateSentence(
    id: string,
    updateSentence: IUpdateSentence
  ): Promise<void> {
    await axios.put<ISentence>(
      import.meta.env.VITE_API_URL + "sentences/" + id,
      updateSentence
    );

    setLessonSampleSentences((prev) => {
      return prev.map((sentence) =>
        sentence.id === id ? { ...sentence, ...updateSentence } : sentence
      );
    });
  }

  async function deleteSentence(id: string): Promise<void> {
    await axios.delete(import.meta.env.VITE_API_URL + "sentences/" + id);

    setLessonSampleSentences((prev) => {
      return prev.filter((sentence) => sentence.id !== id);
    });
  }

  // async function getSentence(id: string): Promise<ISentence> {
  //   const response = await axios.get<ISentence>(
  //     import.meta.env.VITE_API_URL + "sentences/" + id
  //   );

  //   return response.data;
  // }

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
  }, []);

  return {
    lessonSampleSentences,
    // getSentence,
    postSentence,
    updateSentence,
    deleteSentence,
  };
}
