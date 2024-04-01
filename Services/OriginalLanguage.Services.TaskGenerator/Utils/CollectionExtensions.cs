using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Utils;
internal static class CollectionExtensions
{
    public static List<T> Shuffled<T>(
        this IEnumerable<T> list,
        Random? random = null)
    {
        random ??= Random.Shared;
        return list.OrderBy(x => random.Next()).ToList();
    }

    public static T GetRandomElement<T>(
        this List<T> list,
        Random? random = null)
    {
        random ??= new Random();

        if (!list.Any())
            throw new InvalidOperationException(nameof(list));

        int index = random.Next(list.Count);
        return list[index];
    }
}
