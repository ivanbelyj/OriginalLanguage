using OriginalLanguage.Services.Sentences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences;
public interface ISentencesService
{
    Task<SentenceModel> GetSentence(int id);
    Task<IEnumerable<SentenceModel>> GetSentences(int offset = 0, int limit = 10);
    Task<SentenceModel> AddSentence(AddSentenceModel model);
    Task UpdateSentence(int id, UpdateSentenceModel model);
    Task DeleteSentence(int id);
}
