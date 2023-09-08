using System.Net;
using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ResultExtensions
{
    public static ValidationProblemDetails ToProblemDetails<T>(
        this IResult<T> result,
        ControllerBase controllerBase,
        HttpStatusCode? statusCode = null)
    {
        // IEnumerable<ValidationError> empty = Enumerable.Empty<ValidationError>();
        //
        IEnumerable<Error> listErrros = result.Errors ?? Enumerable.Empty<Error>();

        Dictionary<string, string[]> dict = listErrros
            .ToDictionary(k => k.Code!, v => new[] { v.Message! });

        return new(dict)
        {
            Status = (int)(statusCode ?? HttpStatusCode.BadRequest),
            Title = result.Error!.Code,
            Detail = result.Error.Message,
            Instance = controllerBase.HttpContext.Request.Path,
        };
    }

    public static IActionResult MatchPost<T>(this IResult<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.Created("", result.Value),
            ResultStatus.Error => controllerBase.BadRequest(),
            ResultStatus.Invalid => controllerBase.Conflict(
                result.ToProblemDetails(controllerBase, HttpStatusCode.Conflict)),
            _ => throw new ArgumentException("")
        };
    }

    public static IActionResult MatchPut<T>(this IResult<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.NoContent(),
            ResultStatus.Invalid => controllerBase.BadRequest(
                result.ToProblemDetails(controllerBase)),
            ResultStatus.NotFound => controllerBase.NotFound(
                result.ToProblemDetails(controllerBase)),
            _ => throw new ArgumentException("")
        };
    }

    public static IActionResult MatchDelete<T>(this IResult<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.Ok(),
            ResultStatus.Error => controllerBase.BadRequest(
                result.ToProblemDetails(controllerBase)),
            ResultStatus.NotFound => controllerBase.NotFound(
                result.ToProblemDetails(controllerBase)),
            _ => throw new ArgumentException("")
        };
    }

    public static ActionResult<T> MatchFind<T>(this IResult<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.Ok(result.Value),
            ResultStatus.Error => controllerBase.BadRequest(
                result.ToProblemDetails(controllerBase)),
            ResultStatus.NotFound => controllerBase.NotFound(
                result.ToProblemDetails(controllerBase)),
            _ => throw new ArgumentException("")
        };
    }

    public static IActionResult MatchGet<T>(this IResult<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.Ok(result.Value),
            ResultStatus.Error => controllerBase.BadRequest(
                result.ToProblemDetails(controllerBase)),
            _ => throw new ArgumentException("")
        };
    }
}
