using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Languages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages;
public class LanguagesService : ILanguagesService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddLanguageModel> addLanguageModelValidator;
    private readonly IModelValidator<UpdateLanguageModel> updateLanguageModelValidator;
    public LanguagesService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<AddLanguageModel> addLanguageModelValidator,
        IModelValidator<UpdateLanguageModel> updateLanguageModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.addLanguageModelValidator = addLanguageModelValidator;
        this.updateLanguageModelValidator = updateLanguageModelValidator;
    }

    public async Task<LanguageModel> AddLanguage(AddLanguageModel model)
    {
        addLanguageModelValidator.Check(model);

        using MainDbContext dbContext = await dbContextFactory
            .CreateDbContextAsync();

        Language language = mapper.Map<Language>(model);
        await dbContext
            .Languages
            .AddAsync(language);
        dbContext.SaveChanges();

        return mapper.Map<LanguageModel>(language);
    }

    public async Task DeleteLanguage(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        Language? language = dbContext.Languages.FirstOrDefault(x => x.Id == id);
        if (language == null)
            throw new ProcessException($"The language (id: {id}) was not found");

        dbContext.Languages.Remove(language);
        dbContext.SaveChanges();
    }

    public async Task<LanguageModel> GetLanguage(int id)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        Language? language = dbContext
            .Languages
            .Include(x => x.ConlangData)
            .FirstOrDefault(x => x.Id == id);

        return mapper.Map<LanguageModel>(language);
    }

    public async Task<IEnumerable<LanguageModel>> GetUserLanguages(Guid userId)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var languages = await dbContext.Languages
            .Where(lang => lang.AuthorId == userId)
            .Include(x => x.ConlangData)
            .ToListAsync();

        return mapper.Map<IEnumerable<LanguageModel>>(languages);
    }

    public async Task<IEnumerable<LanguageModel>> GetLanguages(int offset = 0,
        int limit = 10)
    {
        return await GetLanguages(x => x, offset, limit);  // No processing
    }

    private async Task<IEnumerable<LanguageModel>> GetLanguages(
        Func<IQueryable<Language>, IQueryable<Language>> processBeforePaging,
        int offset = 0,
        int limit = 10)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var languages = processBeforePaging(dbContext.Languages
            .Include(x => x.ConlangData))
                .Skip(Math.Max(0, offset))
                .Take(Math.Min(Math.Max(0, limit), 1000));

        return (await languages.ToListAsync())
            .Select(mapper.Map<LanguageModel>);
    }

    public async Task<IEnumerable<LanguageModel>> GetLanguagesFiltered(
        LanguagesFilterModel languagesFilterModel,
        int offset = 0,
        int limit = 10)
    {
        return await GetLanguages(lang => ApplyFilter(lang, languagesFilterModel),
            offset, limit);
    }

    private IQueryable<Language> ApplyFilter(
        IQueryable<Language> languages,
        LanguagesFilterModel filter)
    {
        var res = languages;
        if (filter.IsConlang != null)
            res = res.Where(lang => (lang.ConlangDataId != null) == filter.IsConlang);
        
        return res;
    }

    public async Task UpdateLanguage(int id, UpdateLanguageModel model)
    {
        updateLanguageModelValidator.Check(model);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();

        Language? language = await dbContext
            .Languages
            .Include(lang => lang.ConlangData)
            .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ProcessException($"The language (id: {id}) was not found");

        if (model.ConlangData == null && language.ConlangData != null)
        {
            // Todo: fix

            ConlangData conlangData = dbContext
                .ConlangDataEntities
                .First(x => x.Id == language.ConlangDataId);
            dbContext.ConlangDataEntities.Remove(conlangData);
            //language.ConlangData = null;
            //language.ConlangDataId = null;
        }

        Language updatedLanguage = mapper.Map(model, language);
        updatedLanguage.DateTimeUpdated = DateTime.UtcNow;

        dbContext.Languages.Update(updatedLanguage);
        dbContext.SaveChanges();
    }
}
