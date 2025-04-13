namespace Shared.ResultPattern.Models;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public T? Data { get; }

    private Result(bool isSuccess, string error, T? data)
    {
        if (!isSuccess && string.IsNullOrWhiteSpace(error) ||
            isSuccess && !string.IsNullOrWhiteSpace(error))
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }

    public static Result<T> Success(T data) => new(true, string.Empty, data);
    public static Result<T> Failure(string error) => new(false, error, default);
}