using Microsoft.AspNetCore.Mvc;
using Rainfall.Application.Shared;

namespace Rainfall.API.Extensions;

public static class ErrorExtensions
{
    public static IActionResult ToProblemDetails(this Error result)
    {
        var errorResponse = new
        {
            message = result.Message,
            detail = result.Details
                .Select(detail => new
                {
                    propertyName = detail.Target,
                    message = detail.Message
                })
                .ToList()
        };

        return new ObjectResult(errorResponse)
        {
            StatusCode = GetStatusCode(result.Type)
        };

        static int GetStatusCode(ErrorType type)
            => type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}