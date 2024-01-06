using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages.Models;
public class ConlangDataModel
{
    public ConlangType Type { get; set; }
    public ConlangOrigin Origin { get; set; }
    public ArticulationType Articulation { get; set; }
    public SubjectiveComplexity SubjectiveComplexity { get; set; }

    public DevelopmentStatus DevelopmentStatus { get; set; }

    public SettingOrigin SettingOrigin { get; set; }

    public bool NotHumanoidSpeakers { get; set; }
    public bool FurrySpeakers { get; set; }
}

public class ConlangDataModelProfile : Profile
{
    public ConlangDataModelProfile()
    {
        CreateMap<ConlangData, ConlangDataModel>();
        CreateMap<ConlangDataModel, ConlangData>();
    }
}