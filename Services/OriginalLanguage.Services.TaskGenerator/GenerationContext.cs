using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
public class GenerationContext
{
    public int ProgressLevel {  get; set; }
    public LessonSampleModel LessonSample { get; set; }
}
