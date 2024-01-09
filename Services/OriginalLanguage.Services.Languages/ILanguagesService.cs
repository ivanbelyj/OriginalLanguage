using OriginalLanguage.Services.Languages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages;
public interface ILanguagesService
{
    Task<IEnumerable<LanguageModel>> GetLanguages(int offset = 0, int limit = 10);
    Task<IEnumerable<LanguageModel>> GetUserLanguages(Guid userId);
    Task<IEnumerable<LanguageModel>> GetLanguagesFiltered(
        LanguagesFilterModel languagesFilterModel,
        int offset = 0, int limit = 10);
    Task<LanguageModel> GetLanguage(int id);
    Task<LanguageModel> AddLanguage(AddLanguageModel model);
    Task UpdateLanguage(int id, UpdateLanguageModel model);
    Task DeleteLanguage(int id);
}
