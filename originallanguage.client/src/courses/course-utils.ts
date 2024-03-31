import { ICreateCourse } from "./hooks/useCourses";
import ICourse from "./models/ICourse";

export default class CourseUtils {
  static defaultCreateCourseModel(userId: string): ICreateCourse {
    return {
      authorId: userId,
      title: "New Course",
    };
  }
}
