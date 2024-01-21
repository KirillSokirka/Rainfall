using FluentValidation;
using Rainfall.Application.Contracts.DTOs;

namespace Rainfall.API.Validation;

public class RainfallQueryValidator : AbstractValidator<RainfallQuery>
{
    public RainfallQueryValidator()
    {
        RuleFor(x => x.StationId)
            .NotEmpty().WithMessage("The station ID must not be empty.");

        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(0).WithMessage("The count must be zero or a positive number.");
    }
}