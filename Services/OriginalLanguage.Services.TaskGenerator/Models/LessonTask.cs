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

    /// <summary>
    /// The task that needs to be solved
    /// </summary>
    public string? Question { get; set; }

    /// <summary>
    /// Some data given to the user to solve the task.
    /// </summary>
    public string? Given { get; set; }

    /// <summary>
    /// Some lesson tasks can provide helping data,
    /// for example translations according to the given elements
    /// </summary>
    public string? Hint { get; set; }

    /// <summary>
    /// Works similar to Hint property, but related to glossing abbreviations
    /// </summary>
    public string? Glosses { get; set; }

    public TaskType TaskType { get; set; }
}
