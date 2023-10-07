using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class StaticPage : PageBase
{
    public string Name { get; set; }

    public string? MetaKeywords { get; set; }
    public string? MetaDescription { get; set; }

    public StaticPage(string name)
    {
        Name = name;
    }
}
