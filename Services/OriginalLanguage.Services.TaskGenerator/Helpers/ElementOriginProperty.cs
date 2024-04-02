using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Helpers;

/// <summary>
/// Determines on the basis of which sentence property the elements
/// will be obtained.
/// This enum is used instead of passing delegates due to the need of caching
/// elements by their properties
/// </summary>
public enum ElementOriginProperty
{
    Text,
    Translation,
}