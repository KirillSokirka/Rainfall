using Newtonsoft.Json;

namespace Rainfall.Application.Shared;

public sealed record Error
{
    public ErrorType Type { get; }
    public string Message { get; }
    public List<Detail> Details { get; } = new();

    private Error(ErrorType type, string message, List<Detail> details)
    {
        Type = type;
        Message = message;
        Details = details;
    }

    public static readonly Error None = new(ErrorType.None, string.Empty, new List<Detail>());

    public static Error NotFound(string message)
        => new(ErrorType.NotFound, message, new List<Detail>());

    public static Error Validation(string message, List<Detail> details)
        => new(ErrorType.Validation, message, details);

    public static Error Failure(string message, List<Detail> details)
        => new(ErrorType.Validation, message, details);
}

public enum ErrorType
{
    None = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
}