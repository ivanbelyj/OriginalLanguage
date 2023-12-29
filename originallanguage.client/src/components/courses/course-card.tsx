import { ICourse } from "../../models/ICourse";

export const CourseCard: React.FC<{ course: ICourse }> = ({ course }) => {
  return (
    <div>
      <h3>{course.title}</h3>
      <p>Author ID: {course.authorId}</p>
      <p>Language ID: {course.languageId}</p>
      <p>Added: {course.dateTimeAdded?.toString()}</p>
    </div>
  );
};
