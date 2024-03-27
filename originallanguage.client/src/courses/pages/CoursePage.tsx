import React from "react";
import { useParams } from "react-router-dom";
import { CourseTree } from "../components/CourseTree";
import { useLessons } from "../hooks/useLessons";

const CoursePage: React.FC = () => {
  const { id: courseId } = useParams();
  const { courseLessons } = useLessons(courseId!);

  // Todo: handle undefined courseId

  return (
    <div>
      <CourseTree lessons={courseLessons} />
    </div>
  );
};

export default CoursePage;
