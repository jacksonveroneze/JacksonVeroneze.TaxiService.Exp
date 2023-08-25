using System.Net;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ResultExtensions
{
    public static ValidationProblemDetails ToProblemDetails<T>(
        this IResult<T> result,
        ControllerBase controllerBase,
        HttpStatusCode? statusCode = null)
    {
        var empty = Enumerable.Empty<ValidationError>();

        var listErrros = result.ValidationErrors ?? empty;

        var dict = listErrros
            .ToDictionary(k => k.ErrorCode!, v => new[] { v.ErrorMessage! });

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
