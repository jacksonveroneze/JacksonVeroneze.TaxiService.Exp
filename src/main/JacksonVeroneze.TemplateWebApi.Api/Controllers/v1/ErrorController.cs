using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/errors")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class ErrorsController : ControllerBase
{
    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public IResult Error(
        [FromQuery(Name = "error")] int error)
    {
        return Results.StatusCode(error);
    }
}
