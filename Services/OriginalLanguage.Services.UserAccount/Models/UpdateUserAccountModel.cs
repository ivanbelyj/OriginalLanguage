using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.UserAccount.Models;
public class UpdateUserAccountModel
{
    public string Name { get; set; }
}

public class UpdateUserAccountModelValidator : AbstractValidator<UpdateUserAccountModel>
{
    public UpdateUserAccountModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User name is required.");
    }
}

public class UpdateUserAccountModelProfile : Profile
{
    public UpdateUserAccountModelProfile()
    {
        CreateMap<UpdateUserAccountModel, AppUser>()
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.Name));
    }
}
