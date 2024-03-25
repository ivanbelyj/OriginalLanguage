import React from "react";
import { useLessons } from "../hooks/useLessons";
import { useParams } from "react-router-dom";
import { CourseLessons } from "../components/CourseLessons";

const CourseLessonsPage: React.FC = () => {
  const { id: courseId } = useParams();

  // Todo: handle undefined courseId
  const { courseLessons } = useLessons(courseId!);
  return (
    <div>
      <CourseLessons lessons={courseLessons} />
    </div>
  );
};

export default CourseLessonsPage;
