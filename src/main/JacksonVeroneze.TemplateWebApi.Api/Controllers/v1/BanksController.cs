using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/banks")]
[Produces(MediaTypeNames.Application.Json)]
public class BanksController : ControllerBase
{
    private readonly ILogger<BanksController> _logger;
    private readonly IMediator _mediator;

    public BanksController(
        ILogger<BanksController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetPaged")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetBankPagedQuery query)
    {
        _logger.LogGetPaged(nameof(BanksController),
            nameof(GetPagedAsync));

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }

    [HttpGet("{id}", Name = "GetById")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] GetBankByIdQuery query)
    {
        _logger.LogGetById(nameof(BanksController),
            nameof(GetByIdAsync), query.Id);

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }
}
