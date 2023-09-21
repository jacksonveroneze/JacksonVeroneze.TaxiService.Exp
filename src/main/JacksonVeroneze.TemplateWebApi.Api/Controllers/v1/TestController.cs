using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/test")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class TestController : ControllerBase
{
    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public IActionResult GetPage()
    {
        return Ok(DateTime.Now);
    }
}
