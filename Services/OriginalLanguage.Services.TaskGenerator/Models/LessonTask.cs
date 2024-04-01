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
    public string Question { get; set; }

    /// <summary>
    /// Some data given to the user to solve the task.
    /// </summary>
    public string Given { get; set; }

    public TaskType TaskType { get; set; }
}
