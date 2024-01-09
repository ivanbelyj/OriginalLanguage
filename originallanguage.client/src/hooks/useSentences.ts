import { useEffect, useState } from "react";
import axios from "axios";
import ISentence from "../models/ISentence";

export function useSentences(lessonSampleId: string) {
  const [lessonSampleSentences, setLessonSampleSentences] = useState<
    ISentence[]
  >([]);

  async function postSentence(sentence: ISentence): Promise<ISentence> {
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
    updateSentence: ISentence
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

  function fetchSentences() {
    fetch(
      import.meta.env.VITE_API_URL +
        `lesson-samples/${lessonSampleId}/sentences`
    )
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setLessonSampleSentences(data))
      .catch((error) => {
        console.error("There has been a problem with sentences fetch:", error);
      });
  }

  async function getSentence(id: string): Promise<ISentence> {
    const response = await axios.get<ISentence>(
      import.meta.env.VITE_API_URL + "sentences/" + id
    );

    return response.data;
  }

  useEffect(() => {
    fetchSentences();
  }, []);

  return {
    lessonSampleSentences,
    getSentence,
    postSentence,
    updateSentence,
    deleteSentence,
  };
}
