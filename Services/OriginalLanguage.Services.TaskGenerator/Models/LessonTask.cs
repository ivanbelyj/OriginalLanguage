using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
public class LessonTask
{
    public int LessonSampleId { get; set; }
    public string Sentence { get; set; }
    public TaskType TaskType { get; set; }
}
