using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples;
public class LessonSamplesService : ILessonSamplesService
{
    private readonly IMapper mapper;
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IModelValidator<AddLessonSampleModel> addLessonSampleValidator;
    private readonly IModelValidator<UpdateLessonSampleModel> updateLessonSampleValidator;
    
    public LessonSamplesService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<AddLessonSampleModel> addLessonSampleValidator,
        IModelValidator<UpdateLessonSampleModel> updateLessonSampleValidator)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
        this.addLessonSampleValidator = addLessonSampleValidator;
        this.updateLessonSampleValidator = updateLessonSampleValidator;
    }
    public async Task<LessonSampleModel> AddLessonSample(AddLessonSampleModel model)
    {
        addLessonSampleValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var lessonSample = mapper.Map<LessonSample>(model);
        await dbContext.LessonSamples.AddAsync(lessonSample);
        dbContext.SaveChanges();

        return mapper.Map<LessonSampleModel>(lessonSample);
    }

    public async Task DeleteLessonSample(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var lessonSample = await dbContext
            .LessonSamples
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new ProcessException(
                $"The lesson sample (id: {id}) was not found");

        dbContext.LessonSamples.Remove(lessonSample);
        dbContext.SaveChanges();
    }

    public async Task<LessonSampleModel> GetLessonSample(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var lessonSample = dbContext.LessonSamples.FirstOrDefault(x => x.Id == id);

        return mapper.Map<LessonSampleModel>(lessonSample);
    }

    public async Task<IEnumerable<LessonSampleModel>> GetLessonSamples(
        int offset = 0, int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        return (await dbContext
            .LessonSamples
            .Skip(Math.Max(0, offset))
            .Take(Math.Max(0, Math.Min(limit, 1000)))
            .ToListAsync())
            .Select(mapper.Map<LessonSampleModel>);
    }

    public async Task<IEnumerable<LessonSampleModel>> GetSamplesOfLesson(
        int lessonId)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return (await dbContext
            .LessonSamples
            .Where(sample => sample.LessonId == lessonId)
            .ToListAsync())
            .Select(mapper.Map<LessonSampleModel>);
    }

    public async Task UpdateLessonSample(int id, UpdateLessonSampleModel model)
    {
        updateLessonSampleValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var lessonSample = dbContext.LessonSamples.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The lesson sample (id: {id}) was not found");

        dbContext.LessonSamples.Update(mapper.Map(model, lessonSample));
        dbContext.SaveChanges();
    }
}
