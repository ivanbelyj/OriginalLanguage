using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class ConlangData : EntityBase
{
    public ConlangType Type { get; set; }
    public ConlangOrigin Origin { get; set; }
    public ArticulationType Articulation { get; set; }
    public SubjectiveComplexity SubjectiveComplexity { get; set; }

    public DevelopmentStatus DevelopmentStatus { get; set; }

    // Conlang native speakers and setting info
    public SettingOrigin SettingOrigin { get; set; }

    public bool NotHumanoidSpeakers { get; set; }
    public bool FurrySpeakers { get; set; }

    public virtual Language Language { get; set; }
}
