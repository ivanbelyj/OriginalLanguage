using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Courses;
public class CoursesService : ICoursesService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IModelValidator<AddCourseModel> addCourseModelValidator;
    private readonly IModelValidator<UpdateCourseModel> updateCourseModelValidator;

    public CoursesService(IMapper mapper,
        IDbContextFactory<MainDbContext> dbContextFactory,
        IModelValidator<AddCourseModel> addCourseModelValidator,
        IModelValidator<UpdateCourseModel> updateCourseModelValidator)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
        this.addCourseModelValidator = addCourseModelValidator;
        this.updateCourseModelValidator = updateCourseModelValidator;
    }

    public async Task<CourseModel> AddCourse(AddCourseModel model)
    {
        addCourseModelValidator.Check(model);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var course = mapper.Map<Course>(model);
        await dbContext.Courses.AddAsync(course);
        dbContext.SaveChanges();

        return mapper.Map<CourseModel>(course);
    }

    public async Task DeleteCourse(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ProcessException($"The course (id: {id}) was not found");
        dbContext.Courses.Remove(course);
        dbContext.SaveChanges();
    }

    public async Task<CourseModel> GetCourse(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
        return mapper.Map<CourseModel>(course);
    }

    public async Task<IEnumerable<CourseModel>> GetCourses(
        int offset = 0,  int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return await dbContext
            .Courses
            .Skip(Math.Max(0, offset))
            .Take(Math.Min(Math.Max(limit, 0), 1000))
            .Select(x => mapper.Map<CourseModel>(x))
            .ToListAsync();
    }

    public async Task UpdateCourse(int id, UpdateCourseModel model)
    {
        updateCourseModelValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var course = await dbContext.Courses.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ProcessException($"The course (id: {id}) was not found");
        dbContext.Courses.Update(mapper.Map(model, course));
        dbContext.SaveChanges();
    }
}
