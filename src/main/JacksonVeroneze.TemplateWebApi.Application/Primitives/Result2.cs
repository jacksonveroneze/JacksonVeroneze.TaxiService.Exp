using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;

namespace JacksonVeroneze.TemplateWebApi.Application.Primitives;

public class Result
{
    public ResultStatus Status { get; protected set; }
    public Error Errore { get; protected set; }

    public IEnumerable<string> Errors { get; protected set; } = new List<string>();

    public List<ValidationError> ValidationErrors { get; protected set; } = new();

    private Result()
    {
    }

    protected internal Result(ResultStatus status)
    {
        Status = status;
    }

    public static Result Success() => new Result(ResultStatus.Ok);

    public static Result Error(params string[] messages)
    {
        Result result = new(ResultStatus.Error) { Errors = messages };

        return result;
    }

    public static Result Invalid(List<ValidationError> validationErrors)
    {
        Result result = new(ResultStatus.Invalid) { ValidationErrors = validationErrors };

        return result;
    }

    public static Result Invalid(Error error)
    {
        Result result = new(ResultStatus.Invalid) { Errore = error };

        return result;
    }

    public static Result NotFound(params string[] messages)
    {
        Result result = new(ResultStatus.NotFound) { Errors = messages };

        return result;
    }

    public static Result NotFound(Error error)
    {
        Result result = new(ResultStatus.NotFound) { Errore = error };

        return result;
    }

    public static Result Conflict(params string[] messages)
    {
        Result result = new(ResultStatus.Conflict) { Errors = messages };

        return result;
    }

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

public class ValidationError
{
    public string Identifier { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorCode { get; set; }

    // public ValidationSeverity Severity { get; set; }
}
