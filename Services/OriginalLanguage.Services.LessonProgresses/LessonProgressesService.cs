using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonProgresses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses;
public class LessonProgressesService : ILessonProgressesService
{

    private readonly IMapper mapper;
    private readonly IModelValidator<AddLessonProgressModel> addLessonProgressModelValidator;
    private readonly IModelValidator<UpdateLessonProgressModel> updateLessonProgressModelValidator;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    public LessonProgressesService(IMapper mapper,
        IModelValidator<AddLessonProgressModel> addLessonProgressModelValidator,
        IModelValidator<UpdateLessonProgressModel> updateLessonProgressModelValidator,
        IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.addLessonProgressModelValidator = addLessonProgressModelValidator;
        this.updateLessonProgressModelValidator = updateLessonProgressModelValidator;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<LessonProgressModel> AddLessonProgress(AddLessonProgressModel model)
    {
        addLessonProgressModelValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var language = mapper.Map<LessonProgress>(model);
        await dbContext.LessonProgresses.AddAsync(language);
        dbContext.SaveChanges();

        return mapper.Map<LessonProgressModel>(language);
    }

    public async Task DeleteLessonProgress(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lessonProg = dbContext.LessonProgresses.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The lesson progress (id: {id}) was not found");

        dbContext.LessonProgresses.Remove(lessonProg);
        dbContext.SaveChanges();
    }

    public async Task<LessonProgressModel> GetLessonProgress(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lessonProg = dbContext.LessonProgresses.First(x => x.Id == id);
        return mapper.Map<LessonProgressModel>(lessonProg);
    }

    public async Task<LessonProgressModel?> TryGetByUserAndLessonIds(
        Guid userId,
        int lessonId)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lessonProg = dbContext
            .LessonProgresses
            .FirstOrDefault(x => x.LessonId == lessonId && x.UserId == userId);
        return mapper.Map<LessonProgressModel?>(lessonProg);
    }

    public async Task<IEnumerable<LessonProgressModel>> GetLessonProgresses(
        int offset = 0, int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return (await dbContext
            .LessonProgresses
            .Skip(Math.Max(0, offset))
            .Take(Math.Max(0, Math.Min(limit, 1000)))
            .ToListAsync())
            .Select(mapper.Map<LessonProgressModel>);
    }

    public async Task UpdateLessonProgress(int id, UpdateLessonProgressModel model)
    {
        updateLessonProgressModelValidator.Check(model);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var lessonProg = dbContext.LessonProgresses.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The lesson progress (id: {id}) was not found");

        dbContext.LessonProgresses.Update(mapper.Map(model, lessonProg));
        dbContext.SaveChanges();
    }

    public async Task IncrementLessonProgress(int id)
    {
        var lessonProgress = await GetLessonProgress(id);

        var updateModel = mapper.Map<UpdateLessonProgressModel>(lessonProgress);
        updateModel.ProgressLevel++;

        await UpdateLessonProgress(id, updateModel);
    }
}
