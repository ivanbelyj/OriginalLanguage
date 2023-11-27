using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Sentences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences;
public class SentencesService : ISentencesService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddSentenceModel> addSentenceModelValidator;
    private readonly IModelValidator<UpdateSentenceModel> updateSentenceModelValidator;

    public SentencesService(IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper, IModelValidator<AddSentenceModel> addSentenceModelValidator,
        IModelValidator<UpdateSentenceModel> updateSentenceModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.addSentenceModelValidator = addSentenceModelValidator;
        this.updateSentenceModelValidator = updateSentenceModelValidator;
    }

    public async Task<SentenceModel> AddSentence(AddSentenceModel model)
    {
        addSentenceModelValidator.Check(model);

        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var sentence = mapper.Map<Sentence>(model);
        await dbContext.Sentences.AddAsync(sentence);
        dbContext.SaveChanges();

        return mapper.Map<SentenceModel>(sentence);
    }

    public async Task DeleteSentence(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var sentence = dbContext.Sentences.FirstOrDefault(x => x.Id == id)
            ?? throw new ProcessException($"The sentence (id: {id}) was not found");
        dbContext.Sentences.Remove(sentence);
        dbContext.SaveChanges();
    }

    public async Task<SentenceModel> GetSentence(int id)
    {
        // Todo: null safety
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        return mapper.Map<SentenceModel>(
            dbContext.Sentences.FirstOrDefault(x => x.Id == id));
    }

    public async Task<IEnumerable<SentenceModel>> GetSentences(int offset = 0,
        int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        return mapper.Map<IEnumerable<SentenceModel>>(await dbContext
            .Sentences
            .Skip(Math.Max(0, offset))
            .Take(Math.Max(0, Math.Min(1000, limit)))
            .ToListAsync());
    }

    public async Task UpdateSentence(int id, UpdateSentenceModel model)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var sentence = dbContext.Sentences.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception($"The sentence (id: {id}) was not found");
        dbContext.Sentences.Update(mapper.Map(model, sentence));
        dbContext.SaveChanges();
    }
}
