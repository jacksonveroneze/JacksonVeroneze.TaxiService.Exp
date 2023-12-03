using System.Net;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;

public static class ResultExtensions
{
    #region PROBLEM_DETAILS

    private static IResult ToValidationProblem(
        this Result result,
        HttpStatusCode statusCode)
    {
        string message = statusCode switch
        {
            HttpStatusCode.NotFound => "Resource not found.",
            HttpStatusCode.Conflict => "There was a conflict.",
            HttpStatusCode.BadRequest => "Something went wrong.",
            _ => throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, null)
        };

        if (result.Errors is null)
        {
            return Results.Problem(
                detail: result.Error?.Message,
                instance: null,
                statusCode: (int)statusCode,
                title: message
            );
        }

        Dictionary<string, string[]>? dict = result.Errors?
            .ToDictionary(k => k.Code, v => new[] { v.Message });

        return Results.ValidationProblem(
            errors: dict!,
            detail: result.Error?.Message,
            instance: null,
            statusCode: (int)statusCode,
            title: message
        );
    }

    #endregion

    #region GET

    public static IResult MatchGet<T>(this Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            ResultType.Success => result.ToOkEntity(),
            ResultType.Error => result.ToBadRequestEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    public static IResult MatchGetById<T>(this Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            ResultType.Success => result.ToOkEntity(),
            ResultType.Error => result.ToBadRequestEntity(),
            ResultType.NotFound => result.ToNotFoundEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region DELETE

    public static IResult MatchDelete<T>(this Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            ResultType.Success => Results.NoContent(),
            ResultType.Error or ResultType.Invalid =>
                Results.BadRequest(result.ToValidationProblem(HttpStatusCode.BadRequest)),
            ResultType.NotFound => Results.NotFound(result.ToValidationProblem(HttpStatusCode.NotFound)),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region PUT

    public static IResult MatchPut<T>(this Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            ResultType.Success => result.ToNoContentEntity(),
            ResultType.Invalid => result.ToConflictEntity(),
            ResultType.Error => result.ToBadRequestEntity(),
            ResultType.NotFound => result.ToNotFoundEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region POST

    public static IResult MatchPost<T>(this Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            ResultType.Success => result.ToCreatedEntity(),
            ResultType.Invalid => result.ToConflictEntity(),
            ResultType.Error => result.ToBadRequestEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    private static IResult ToOkEntity<T>(this Result<T> result)
    {
        return Results.Ok(result.Value);
    }

    private static IResult ToCreatedEntity<T>(this Result<T> result)
    {
        return Results.Created(string.Empty, result.Value);
    }

    private static IResult ToNoContentEntity(this Result result)
    {
        return Results.NoContent();
    }

    private static IResult ToNotFoundEntity(this Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.NotFound);
    }

    private static IResult ToConflictEntity(this Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.Conflict);
    }

    private static IResult ToUnprocessableEntity(this Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.UnprocessableEntity);
    }

    private static IResult ToBadRequestEntity(this Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.BadRequest);
    }
}
