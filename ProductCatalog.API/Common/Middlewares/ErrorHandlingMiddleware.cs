using System.Text.Json;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Exceptions;
using ProductCatalog.Application.Logging;

namespace ProductCatalog.API.Common.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, ILoggerManager<ErrorHandlingMiddleware> loggerManager)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception e)
        {
            loggerManager.LogError(e?.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = GetTitle(exception),
            status = statusCode,
            detail = exception.Message,
            errors = AssignErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
           exception switch
           {
               InvalidOperationException => StatusCodes.Status400BadRequest,
               ArgumentNullException => StatusCodes.Status400BadRequest,
               ArgumentException => StatusCodes.Status400BadRequest, 
               ValidatorException => StatusCodes.Status400BadRequest,
               NotFoundException e => (int)e.StatusCode,
               BadRequestException e => (int)e.StatusCode,
               _ => StatusCodes.Status500InternalServerError
           };

    public static string GetTitle(Exception exception) =>
        exception switch
        {
            NotFoundException notFoundException => notFoundException.Title,
            BadRequestException badRequestException => badRequestException.Title, 
            _ => Strings.ServerError
        };

    public static IReadOnlyList<string>? AssignErrors(Exception exception)
    {
        IReadOnlyList<string>? errors = null;

        if (exception is ValidatorException validatorException)
        {
            errors = validatorException.Errors;
        }

        return errors;
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}