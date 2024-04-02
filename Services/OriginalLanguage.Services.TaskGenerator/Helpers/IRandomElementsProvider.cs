using OriginalLanguage.Services.Sentences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Helpers;

/// <summary>
/// Provides random extra elements for task generation needs in an optimized
/// and ready for using in generation handlers way
/// </summary>
public interface IRandomElementsProvider
{
    Task<List<string>> GetRandomElements(
        int lessonId,
        ElementOriginProperty property,
        int limit);
}
