namespace Rainfall.Application.Shared;

public class Result<TValue>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public TValue Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException("There is no value for a failure result.");
            }

            return _value;
        }
    }

    private readonly TValue _value;

    private Result(bool isSuccess, TValue? value, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None ||
            isSuccess && value is null)
        {
            throw new ArgumentException("Invalid argument error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        _value = value!;
    }

    public static Result<TValue> Success(TValue value) => new(true, value, Error.None);
    public static Result<TValue> Failure(Error error) => new(false, default, error);
    
    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => Failure(error);

}