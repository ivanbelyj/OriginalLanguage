using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Helpers;
public class RandomElementsProvider : IRandomElementsProvider
{
    private readonly LessonElementsHelper randomElementsHelper;

    private Dictionary<(int, ElementOriginProperty), List<string>> elementsByLessonId
        = new();

    public RandomElementsProvider(LessonElementsHelper randomElementsHelper)
    {
        this.randomElementsHelper = randomElementsHelper;
    }

    public async Task<List<string>> GetRandomElements(
        int lessonId,
        ElementOriginProperty property,
        int limit)
    {
        List<string> lessonElements;
        if (elementsByLessonId.TryGetValue(
            (lessonId, property),
            out var cachedLessonElements)) {
            lessonElements = cachedLessonElements;
        } else
        {
            lessonElements = (await randomElementsHelper.GetAllLessonElements(
                lessonId,
                ElementOriginPropertyUtils.ToFunc(property)))
                .ToList();
            elementsByLessonId.Add((lessonId, property), lessonElements);
        }

        return lessonElements
            .Shuffled()
            .Take(limit)
            .ToList();
    }

    
}
