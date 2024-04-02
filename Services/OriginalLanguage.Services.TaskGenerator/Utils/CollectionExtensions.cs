using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Utils;
internal static class CollectionExtensions
{
    public static List<T> Shuffled<T>(
        this IEnumerable<T> seq,
        Random? random = null)
    {
        random ??= Random.Shared;
        return seq.OrderBy(x => random.Next()).ToList();
    }

    public static T GetRandomElement<T>(
        this IEnumerable<T> seq,
        Random? random = null)
    {
        random ??= new Random();

        if (!seq.Any())
            throw new InvalidOperationException(nameof(seq));

        int index = random.Next(seq.Count());
        return seq.ElementAt(index);
    }
}
