using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ResultToActionResult
{
    public static ActionResult ToResult(this Result result, ControllerBase controller)
    {
        return result.ToMinimalApiResult(controller);
    }

    private static ActionResult ToMinimalApiResult(this Result result, ControllerBase controller)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                return controller.NoContent();
            case ResultStatus.Error:
                return controller.UnprocessableEntity(result.ValidationErrors);
            case ResultStatus.Invalid:
                return controller.BadRequest(result.ValidationErrors);
            case ResultStatus.NotFound:
                return controller.NotFound(result.Errors);
            case ResultStatus.Conflict:
                return controller.Conflict(result.Errors);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
