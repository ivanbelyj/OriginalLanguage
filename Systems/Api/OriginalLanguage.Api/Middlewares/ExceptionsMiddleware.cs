using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Extensions;
using OriginalLanguage.Common.Responses;

namespace OriginalLanguage.Api.Middlewares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate next;
    public ExceptionsMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse? errorResponse = null;
        try {
            await next.Invoke(context);
        } catch (ProcessException exception)
        {
            errorResponse = exception.ToErrorResponse();
        } catch (Exception ex)
        {
            errorResponse = ex.ToErrorResponse();
        } finally
        {
            if (errorResponse != null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(errorResponse);
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}
