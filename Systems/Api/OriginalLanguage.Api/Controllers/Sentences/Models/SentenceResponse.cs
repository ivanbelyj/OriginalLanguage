﻿using AutoMapper;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences.Models;

public class SentenceResponse
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public int LessonSampleId { get; set; }
}

class SentenceResponseProfile : Profile
{
    public SentenceResponseProfile()
    {
        CreateMap<SentenceModel, SentenceResponse>();
    }
}