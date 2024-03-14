import { useEffect, useState } from "react";
import EditCourse from "../components/courses/EditCourse";
import EditCourseLessons from "../components/courses/EditCourseLessons";
import ILesson from "../models/ILesson";
import { useLessons } from "../hooks/lessons";
import { useParams } from "react-router-dom";
import ILessonSample from "../models/ILessonSample";
import ISentence from "../models/ISentence";
import { message } from "antd";
import { useLessonSamples } from "../hooks/useLessonSamples";
import { useSentences } from "../hooks/useSentences";

const EditCoursePage = () => {
  const [messageApi, messageContextHolder] = message.useMessage();

  const { id: courseId } = useParams();

  // Edited / added data
  const { courseLessons, postLesson } = useLessons(courseId!);
  const [editedLessons, setEditedLessons] = useState<ILesson[]>(courseLessons);

  const { postLessonSample } = useLessonSamples();
  const [editedLessonSamples, setEditedLessonSamples] =
    useState<ILessonSample[]>();

  const { postSentence } = useSentences();
  const [editedSentences, setEditedSentences] = useState<ISentence[]>();

  // const lessonsToAdd: ICreateLesson[] = [];
  // const lessonSamplesToAdd: ICreateLessonSample[] = [];
  // const sentencesToAdd: ICreateSentence[] = [];

  useEffect(() => {
    setEditedLessons(courseLessons);
  }, [courseLessons]);

  // #region Add
  const handleAddLesson = async () => {
    if (!courseId) return;

    console.log("Add lesson");
    await postLesson({
      courseId: courseId,
      number:
        editedLessons.reduce(
          (acc, lesson) => (lesson.number > acc ? lesson.number : acc),
          0
        ) + 1,
    });

    // messageApi.open({
    //   type: "success",
    //   content: "Lesson is added",
    // });
  };

  const handleAddLessonSample = async (lessonId: string) => {
    await postLessonSample({
      lessonId,
      minimalProgressLevel: 0,
    });
  };
  const handleAddSentence = async (lessonSampleId: string) => {
    await postSentence({
      lessonSampleId,
    });
    // console.log("Todo: Add sentence to sample ", lessonSampleId);
    // sentencesToAdd.push({
    //   lessonSampleId,
    // });
  };
  // #endregion

  // #region Get / edit
  const getLessonById = (id: string): ILesson => {
    return editedLessons.find((lesson) => lesson.id === id)!;
  };

  const setLessonById = (id: string, lesson: ILesson): void => {
    const index = editedLessons.findIndex((l) => l.id === id);
    if (index !== -1) {
      const newLessons = [...editedLessons];
      newLessons[index] = lesson;
      setEditedLessons(newLessons);
    }
  };

  const getLessonSampleById = (id: string): ILessonSample => {
    return editedLessonSamples?.find((sample) => sample.id === id)!;
  };

  const setLessonSampleById = (id: string, sample: ILessonSample): void => {
    if (editedLessonSamples) {
      const index = editedLessonSamples.findIndex((s) => s.id === id);
      if (index !== -1) {
        const newSamples = [...editedLessonSamples];
        newSamples[index] = sample;
        setEditedLessonSamples(newSamples);
      }
    }
  };

  const getSentenceById = (id: string): ISentence => {
    return editedSentences?.find((sentence) => sentence.id === id)!;
  };

  const setSentenceById = (id: string, sentence: ISentence): void => {
    if (editedSentences) {
      const index = editedSentences.findIndex((s) => s.id === id);
      if (index !== -1) {
        const newSentences = [...editedSentences];
        newSentences[index] = sentence;
        setEditedSentences(newSentences);
      }
    }
  };
  // #endregion

  const saveCourse = () => {
    console.log("save course");
  };

  return (
    <>
      {messageContextHolder}
      <EditCourse saveCourse={saveCourse} />

      {editedLessons && (
        <EditCourseLessons
          lessons={editedLessons}
          setLessons={setEditedLessons}
          setLessonSampleById={setLessonSampleById}
          setSentenceById={setSentenceById}
          getLessonSampleById={getLessonSampleById}
          getSentenceById={getSentenceById}
          handleAddLesson={handleAddLesson}
          handleAddLessonSample={handleAddLessonSample}
          handleAddSentence={handleAddSentence}
        />
      )}
    </>
  );
};

export default EditCoursePage;
