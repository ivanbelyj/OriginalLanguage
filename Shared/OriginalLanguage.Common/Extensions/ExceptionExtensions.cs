namespace OriginalLanguage.Common.Extensions;

using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Responses;
using FluentValidation.Results;

public static class ErrorResponseExtensions
{
    /// <summary>
    /// Make ErrorResponse from ValidationResult
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ValidationResult data)
    {
        var res = new ErrorResponse()
        {
            Message = "",
            FieldErrors = data.Errors.Select(x =>
            {
                var elems = x.ErrorMessage.Split('&');
                var errorName = elems[0];
                var errorMessage = elems.Length > 0 ? elems[1] : errorName;
                return new ErrorResponseFieldInfo()
                {
                    FieldName = x.PropertyName,
                    Message = errorMessage,
                };
            })
        };

        return res;
    }

    /// <summary>
    /// Convert ProcessException to ErrorResponse
    /// </summary>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this ProcessException data)
    {
        var res = new ErrorResponse()
        {
            Message = data.Message
        };

        return res;
    }

    /// <summary>
    /// Convert exception to ErrorResponse
    /// </summary>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this Exception data)
    {
        var res = new ErrorResponse()
        {
            ErrorCode = -1,
            Message = data.Message
        };

        return res;
    }
}
