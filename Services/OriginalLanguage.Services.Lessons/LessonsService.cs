using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Lessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons;
public class LessonsService : ILessonsService
{
    private readonly IMapper mapper;
    private readonly IModelValidator<AddLessonModel> addLessonModelValidator;
    private readonly IModelValidator<UpdateLessonModel> updateLessonModelValidator;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    public LessonsService(IMapper mapper,
        IModelValidator<AddLessonModel> addLessonModelValidator,
        IModelValidator<UpdateLessonModel> updateLessonModelValidator,
        IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.addLessonModelValidator = addLessonModelValidator;
        this.updateLessonModelValidator = updateLessonModelValidator;
        this.dbContextFactory = dbContextFactory;
    }

    // Todo: adding limit
    public async Task<LessonModel> AddLesson(AddLessonModel model)
    {
        addLessonModelValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lesson = mapper.Map<Lesson>(model);
        await dbContext.Lessons.AddAsync(lesson);
        dbContext.SaveChanges();

        return mapper.Map<LessonModel>(lesson);
    }

    public async Task DeleteLesson(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lesson = dbContext.Lessons.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The lesson (id: {id}) was not found");
        
        dbContext.Lessons.Remove(lesson);
        dbContext.SaveChanges();
    }

    public async Task<IEnumerable<LessonModel>> GetCourseLessons(int courseId)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lessons = dbContext
            .Lessons
            .Where(lesson => lesson.CourseId == courseId);
        return (await lessons.ToListAsync())
            .Select(mapper.Map<LessonModel>);
    }

    public async Task<LessonModel> GetLesson(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lesson = dbContext.Lessons.FirstOrDefault(x => x.Id == id);
        return mapper.Map<LessonModel>(lesson);
    }

    public async Task<IEnumerable<LessonModel>> GetLessons(
        int offset = 0, int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return (await dbContext
            .Lessons
            .Skip(Math.Max(0, offset))
            .Take(Math.Max(0, Math.Min(limit, 1000)))
            .ToListAsync())
            .Select(mapper.Map<LessonModel>);
    }

    public async Task UpdateLesson(int id, UpdateLessonModel model)
    {
        updateLessonModelValidator.Check(model);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var lesson = dbContext.Lessons.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The lesson (id: {id}) was not found");

        dbContext.Lessons.Update(mapper.Map(model, lesson));
        dbContext.SaveChanges();
    }
}
