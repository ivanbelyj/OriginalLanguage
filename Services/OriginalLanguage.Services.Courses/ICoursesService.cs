using OriginalLanguage.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Courses;
public interface ICoursesService
{
    Task<CourseModel> GetCourse(int id);
    Task<IEnumerable<CourseModel>> GetCourses(int offset = 0, int limit = 10);
    Task<CourseModel> AddCourse(AddCourseModel model);
    Task UpdateCourse(int id, UpdateCourseModel model);
    Task DeleteCourse(int id);
}
