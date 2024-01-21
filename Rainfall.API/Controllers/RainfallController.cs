using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Rainfall.API.Extensions;
using Rainfall.Application.Contracts.DTOs;
using Rainfall.Application.Contracts.Services;
using Rainfall.Application.Shared;

namespace Rainfall.API.Controllers;

[ApiController]
[Route("rainfall")]
public class RainfallController : ControllerBase
{
    private readonly IRainfallDataService _rainfallDataService;
    private readonly IValidator<RainfallQuery> _validator;

    public RainfallController(IRainfallDataService rainfallDataService, IValidator<RainfallQuery> validator)
    {
        _rainfallDataService = rainfallDataService;
        _validator = validator;
    }

    [HttpGet("{stationId}/readings")]
    public async Task<IActionResult> GetReadings(CancellationToken cancellationToken, [FromRoute] string stationId,
        [FromQuery] int count = 10)
    {
        var rainfallQuery = new RainfallQuery(stationId, count);

        var validationResult = await _validator.ValidateAsync(rainfallQuery, cancellationToken);

        if (validationResult.Errors.Any())
        {
            var errorDetails = validationResult.Errors
                .Select(m => new Detail(m.PropertyName, m.ErrorMessage))
                .ToList();

            var error = Error.Validation("The validation error occured", errorDetails);

            return error.ToProblemDetails();
        }

        var result = await _rainfallDataService.GetRainfallDataAsync(rainfallQuery);

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToProblemDetails();
    }
}