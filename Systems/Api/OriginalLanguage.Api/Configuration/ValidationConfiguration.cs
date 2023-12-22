using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OriginalLanguage.Common.Utils;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Common;
using FluentValidation.AspNetCore;

namespace OriginalLanguage.Api.Configuration;

public static class ValidationConfiguration
{
    public static IMvcBuilder AddAppValidation(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var fieldErrors = new List<ErrorResponseFieldInfo>();
                foreach (var (field, state) in context.ModelState)
                {
                    if (state.ValidationState == ModelValidationState.Invalid)
                        fieldErrors.Add(new ErrorResponseFieldInfo()
                        {
                            FieldName = field.ToCamelCase(),
                            Message = string.Join(", ", state.Errors.Select(x => x.ErrorMessage))
                        });
                }

                var result = new BadRequestObjectResult(new ErrorResponse()
                {
                    ErrorCode = 100,
                    Message = "One or more validation errors occurred.",
                    FieldErrors = fieldErrors
                });

                return result;
            };
        });

        //builder.AddFluentValidation(fv =>
        //{
        //    fv.DisableDataAnnotationsValidation = true;
        //    fv.ImplicitlyValidateChildProperties = true;
        //    fv.AutomaticValidationEnabled = true;
        //});


        builder
            .Services
            .AddFluentValidationAutoValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                //fv.ImplicitlyValidateChildProperties = true;
            });


        FluentValidationUtils.AddAppValidators(builder.Services);

        builder.Services.AddSingleton(typeof(IModelValidator<>),
            typeof(ModelValidator<>));

        return builder;
    }
}
