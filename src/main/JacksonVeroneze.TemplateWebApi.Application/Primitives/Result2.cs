namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public class Result
{
    public ResultStatus Status { get; set; }

    public Result()
    {
    }

    protected internal Result(ResultStatus status)
    {
        Status = status;
    }

    public static Result NotFound() => new Result(ResultStatus.NotFound);

    public static Result Success() => new Result(ResultStatus.Ok);

    public static Result Conflict() => new Result(ResultStatus.Conflict);

    public static Result Error() => new Result(ResultStatus.Error);

    public static Result Created() => new Result(ResultStatus.Created);
}

public enum ResultStatus
{
    Ok,
    Error,
    Created,
    Forbidden,
    Unauthorized,
    Invalid,
    NotFound,
    Conflict,
}
