import React from "react";
import { CourseLessons } from "../../components/courses/CourseLessons";
import { useLessons } from "../../hooks/useLessons";
import { useParams } from "react-router-dom";

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
