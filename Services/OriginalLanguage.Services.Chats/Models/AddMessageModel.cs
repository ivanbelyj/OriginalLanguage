﻿using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats.Models;

#nullable disable
/// <summary>
/// Message model contains minimal data about a message
/// </summary>
public class AddMessageModel
{
    public string Content { get; set; }
    public string? UserId { get; set; }
    public DateTime DateTime { get; set; }
    public string GroupId { get; set; }
}

public class AddMessageModelValidator : AbstractValidator<AddMessageModel>
{
    public AddMessageModelValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("GroupId is required");
    }
}

public class AddMessageModelProfile : Profile
{
    public AddMessageModelProfile()
    {
        CreateMap<AddMessageModel, ChatMessage>();
    }
}
