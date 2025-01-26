using System.Net;
using Microsoft.AspNetCore.Http;
using Result = JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseApi.Endpoints.Extensions;

public static class ResultExtensions
{
    #region PROBLEM_DETAILS

    private static IResult ToValidationProblem(
        this Result.Result result,
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

    public static IResult MatchGet<T>(this Result.Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => result.ToOkEntity(),
            Result.ResultType.Error => result.ToBadRequestEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    public static IResult MatchGetById<T>(this Result.Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => result.ToOkEntity(),
            Result.ResultType.Error => result.ToBadRequestEntity(),
            Result.ResultType.NotFound => result.ToNotFoundEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region DELETE

    public static IResult MatchDelete(this Result.Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => Results.NoContent(),
            Result.ResultType.Error or Result.ResultType.Invalid =>
                Results.BadRequest(result.ToValidationProblem(HttpStatusCode.BadRequest)),
            Result.ResultType.NotFound => Results.NotFound(
                result.ToValidationProblem(HttpStatusCode.NotFound)),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region PUT

    public static IResult MatchPut(this Result.Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => ToNoContentEntity(),
            Result.ResultType.Invalid => result.ToConflictEntity(),
            Result.ResultType.Error => result.ToBadRequestEntity(),
            Result.ResultType.NotFound => result.ToNotFoundEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    #region POST

    public static IResult MatchPost(this Result.Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => ToCreatedEntity(),
            Result.ResultType.Invalid => result.ToConflictEntity(),
            Result.ResultType.Error => result.ToBadRequestEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    public static IResult MatchPost<T>(this Result.Result<T> result) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Type switch
        {
            Result.ResultType.Success => result.ToCreatedEntity(),
            Result.ResultType.Invalid => result.ToConflictEntity(),
            Result.ResultType.Error => result.ToBadRequestEntity(),
            _ => throw new ArgumentException(nameof(result.Type))
        };
    }

    #endregion

    private static IResult ToOkEntity<T>(this Result.Result<T> result)
    {
        return Results.Ok(result.Value);
    }

    private static IResult ToCreatedEntity<T>(this Result.Result<T> result)
    {
        return Results.Json(result.Value,
            statusCode: (int)HttpStatusCode.Created);
    }

    private static IResult ToCreatedEntity()
    {
        return Results.Created();
    }

    private static IResult ToNoContentEntity()
    {
        return Results.NoContent();
    }

    private static IResult ToNotFoundEntity(this Result.Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.NotFound);
    }

    private static IResult ToConflictEntity(this Result.Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.Conflict);
    }

    // private static IResult ToUnprocessableEntity(this Result result)
    // {
    //     return result.ToValidationProblem(
    //         HttpStatusCode.UnprocessableEntity);
    // }

    private static IResult ToBadRequestEntity(this Result.Result result)
    {
        return result.ToValidationProblem(
            HttpStatusCode.BadRequest);
    }
}