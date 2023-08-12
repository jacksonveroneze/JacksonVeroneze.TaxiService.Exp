using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ResultExtensions
{
    public static ValidationProblemDetails ToProblemDetails<T>(
        this Result<T> result, ControllerBase controllerBase)
    {
        var empty = Enumerable.Empty<ValidationError>();

        var listErrros = result.ValidationErrors ?? empty;

        var dict = listErrros
            .ToDictionary(k => k.ErrorCode!, v => new[] { v.ErrorMessage! });

        return new(dict)
        {
            Status = 400,
            Title = result!.Error!.Code,
            Detail = result.Error.Message,
            Instance = controllerBase.HttpContext.Request.Path,
        };
    }

    public static IActionResult MatchPost<T>(this Result<T> result,
        ControllerBase controllerBase) where T : class
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(controllerBase);

        return result.Status switch
        {
            ResultStatus.Success => controllerBase.Created("", result.Value),
            ResultStatus.Error => controllerBase.BadRequest(),
            _ => throw new ArgumentException("")
        };
    }

    public static IActionResult MatchPut<T>(this Result<T> result,
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

    public static IActionResult MatchDelete<T>(this Result<T> result,
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

    public static IActionResult MatchFind<T>(this Result<T> result,
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

    public static IActionResult MatchGet<T>(this Result<T> result,
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
