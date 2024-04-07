import { ICreateCourse } from "./hooks/useCourses";
import ILesson from "./models/ILesson";

export default class CourseUtils {
  static defaultCreateCourseModel(userId: string): ICreateCourse {
    return {
      authorId: userId,
      title: "New Course",
    };
  }

  static getLessonTitle(lesson: ILesson) {
    return lesson.title || `Lesson ${lesson.number}`;
  }
}
