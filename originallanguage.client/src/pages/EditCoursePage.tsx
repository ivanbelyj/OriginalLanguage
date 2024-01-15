import { useEffect, useState } from "react";
import EditCourse from "../components/courses/EditCourse";
import EditCourseLessons from "../components/courses/EditCourseLessons";
import ILesson from "../models/ILesson";
import { ICreateLesson, useLessons } from "../hooks/lessons";
import { useParams } from "react-router-dom";
import {
  ICreateLessonSample,
  useLessonSamples,
} from "../hooks/useLessonSamples";
import ILessonSample from "../models/ILessonSample";
import ISentence from "../models/ISentence";
import { ICreateSentence } from "../hooks/useSentences";

const EditCoursePage = () => {
  const { id: courseId } = useParams();
  if (!courseId) return <div>Todo: handle empty course id</div>;

  const { courseLessons } = useLessons(courseId);
  const [editedLessons, setEditedLessons] = useState<ILesson[]>(courseLessons);

  const [editedLessonSamples, setEditedLessonSamples] =
    useState<ILessonSample[]>();
  const [editedSentences, setEditedSentences] = useState<ILessonSample[]>();

  const lessonsToAdd: ICreateLesson[] = [];
  const lessonSamplesToAdd: ICreateLessonSample[] = [];
  const sentencesToAdd: ICreateSentence[] = [];

  const handleAddLesson = () => {
    console.log("Todo: Add lesson");
    lessonsToAdd.push({
      courseId: courseId,
      number: 0, // Todo: set next number
    });
  };
  const handleAddLessonSample = (lessonId: string) => {
    console.log("Todo: Add lesson sample to lesson ", lessonId);
    lessonSamplesToAdd.push({
      lessonId,
      minimalProgressLevel: 0,
    });
  };
  const handleAddSentence = (lessonSampleId: string) => {
    console.log("Todo: Add sentence to sample ", lessonSampleId);
    sentencesToAdd.push({
      lessonSampleId,
    });
  };

  useEffect(() => {
    setEditedLessons(courseLessons);
  }, [courseLessons]);

  return (
    <>
      <EditCourse />

      {editedLessons && (
        <EditCourseLessons
          lessons={editedLessons}
          setLessons={setEditedLessons}
          handleAddLesson={handleAddLesson}
          handleAddLessonSample={handleAddLessonSample}
          handleAddSentence={handleAddSentence}
        />
      )}
    </>
  );
};

export default EditCoursePage;
