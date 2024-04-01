using OriginalLanguage.Services.TaskGenerator.Helpers;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
internal class ElementsToTextHandler : GenerationHandlerBase
{
    private readonly int lessonId;
    private readonly RandomElementsHelper randomElementsHelper;

    public ElementsToTextHandler(
        int lessonId,
        RandomElementsHelper randomElementsHelper,
        int progressLevel) : base(progressLevel)
    {
        this.lessonId = lessonId;
        this.randomElementsHelper = randomElementsHelper;
    }

    public override async Task<string> GenerateQuestion(string[] elements)
    {
        var res = new List<string>();
        res.AddRange(elements.Select(x => x.ToLower()));

        var randomElements = await randomElementsHelper
            .GetRandomElements(lessonId, x => x.Text, 10);
        res.AddRange(randomElements);

        return string.Join(" ", res.Distinct().Shuffled());
    }
}
