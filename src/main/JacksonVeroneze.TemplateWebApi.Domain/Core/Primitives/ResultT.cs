using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public class Result<TValue> : IResult<TValue> where TValue : class
{
    public ResultStatus Status { get; }

    public TValue? Value { get; }

    public Error? Error { get; set; }

    public IList<ValidationError>? ValidationErrors { get; }

    public bool IsSuccess => Status is ResultStatus.Success;

    public bool IsNotSuccess => !IsSuccess;

    #region ctor

    private Result()
    {
    }

    private Result(ResultStatus status)
    {
        Status = status;
    }

    private Result(ResultStatus status, TValue value)
    {
        Status = status;
        Value = value;
    }

    private Result(ResultStatus status, Error error)
    {
        Status = status;
        Error = error;
    }

    private Result(ResultStatus status,
        IList<ValidationError> validationErrors)
    {
        Status = status;
        ValidationErrors = validationErrors;
    }

    #endregion

    #region Success

    public static IResult<TValue> Success()
    {
        return new Result<TValue>(ResultStatus.Success);
    }

    public static IResult<TValue> Success(TValue value)
    {
        return new Result<TValue>(ResultStatus.Success, value);
    }

    #endregion

    #region NotFound

    public static IResult<TValue> NotFound(Error error)
    {
        return new Result<TValue>(ResultStatus.NotFound, error);
    }

    #endregion

    #region Invalid

    public static IResult<TValue> Invalid(Error error)
    {
        return new Result<TValue>(ResultStatus.Invalid, error);
    }

    public static IResult<TValue> Invalid(
        IList<ValidationError> validationErrors)
    {
        return new Result<TValue>(ResultStatus.Invalid,
            validationErrors);
    }

    #endregion
}
