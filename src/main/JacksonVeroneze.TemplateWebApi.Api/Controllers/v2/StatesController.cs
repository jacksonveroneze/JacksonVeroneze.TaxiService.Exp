using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.State;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("/api/v{version:apiVersion}/states")]
[Produces(MediaTypeNames.Application.Json)]
public class StatesController : ControllerBase
{
    private readonly ILogger<StatesController> _logger;
    private readonly IMediator _mediator;

    public StatesController(
        ILogger<StatesController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetPaged")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetStatePagedQuery query)
    {
        _logger.LogGetPagedStates(nameof(StatesController),
            nameof(GetPagedAsync));

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }
}
