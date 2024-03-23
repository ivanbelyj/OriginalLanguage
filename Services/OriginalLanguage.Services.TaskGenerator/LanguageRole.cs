using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
/// <summary>
/// Target language is a language being studied.
/// Source language is a language understandable for user and used for studying.
/// A sentence on the target language is called <b>text</b>
/// and a sentence on the source language is called <b>translation</b>
/// </summary>
internal enum LanguageRole
{
    Source,
    Target
}
