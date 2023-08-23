using JacksonVeroneze.NET.DomainObjects;

namespace JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

public class Result : IResult
{
    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IList<ValidationError>? ValidationErrors { get; }

    #region ctor

    private Result()
    {
    }

    private Result(ResultStatus status)
    {
        Status = status;
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

    public static IResult Success()
    {
        return new Result(ResultStatus.Success);
    }

    #endregion

    #region Invalid

    public static IResult Invalid(Error error)
    {
        return new Result(ResultStatus.Invalid, error);
    }

    #endregion
}
