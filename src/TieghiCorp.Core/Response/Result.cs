using System.Text.Json.Serialization;

namespace TieghiCorp.Core.Response;

public class Result
{
    public bool IsSuccess { get; }

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    [JsonIgnore]
    public HttpError Error { get; }

    public Result(bool isSuccess, HttpError error)
    {
        if (isSuccess && error != HttpError.None || !isSuccess && error == HttpError.None)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, HttpError.None);

    public static Result Failure(HttpError error) => new(false, error);
}

public class Result<TData> : Result
{
    public TData? Data { get; }

    public Result(TData data) : base(true, HttpError.None) => Data = data;
    public Result(HttpError error) : base(false, error) => Data = default;

    public static Result<TData> Success(TData data) => new(data);
    public static new Result<TData> Failure(HttpError error) => new(error);
}