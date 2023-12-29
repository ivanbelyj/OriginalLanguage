import { ICourse } from "../models/ICourse";

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <div>
      <h2>{course.title}</h2>
      <p>Author ID: {course.authorId}</p>
      <p>Language ID: {course.languageId}</p>
      <p>Added: {course.dateTimeAdded?.toString()}</p>
    </div>
  );
};
