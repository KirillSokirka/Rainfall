using System.Net;
using Newtonsoft.Json;

namespace Rainfall.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = exception switch
        {
            HttpRequestException httpEx => httpEx.StatusCode ?? HttpStatusCode.InternalServerError,
            JsonException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var result = JsonConvert.SerializeObject(new
        {
            message = "Unexpected error occured",
            error = new
            {
                message = exception.Message,
                detail = exception.InnerException?.Message
            }
        });

        return context.Response.WriteAsync(result);
    }
}