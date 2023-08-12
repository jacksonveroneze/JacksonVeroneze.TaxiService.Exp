using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public class Result<TValue>
{
    public ResultStatus Status { get; }

    public TValue? Value { get; }

    public Error? Error { get; set; }

    public List<ValidationError>? ValidationErrors { get; protected set; }

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

    #endregion

    #region Success

    public static Result<TValue> Success()
    {
        return new Result<TValue>(ResultStatus.Success);
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(ResultStatus.Success, value);
    }

    #endregion

    #region NotFound

    public static Result<TValue> NotFound(Error error)
    {
        return new Result<TValue>(ResultStatus.NotFound, error);
    }

    #endregion

    #region Invalid

    public static Result<TValue> Invalid(Error error)
    {
        return new Result<TValue>(ResultStatus.Invalid, error);
    }

    public static Result<TValue> Invalid(List<ValidationError> validationErrors)
    {
        return new(ResultStatus.Invalid) { ValidationErrors = validationErrors };
    }

    #endregion
}
