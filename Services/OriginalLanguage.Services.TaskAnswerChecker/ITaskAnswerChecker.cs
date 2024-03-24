using OriginalLanguage.Services.TaskAnswerChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker;
public interface ITaskAnswerChecker
{
    Task<CheckAnswerResult> Check(TaskAnswer answer);
}
