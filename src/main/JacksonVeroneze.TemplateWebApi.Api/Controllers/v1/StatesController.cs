using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.City;
using JacksonVeroneze.TemplateWebApi.Application.Queries.State;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[Route("/api/v1/states")]
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
        _logger.LogGetAllStates(nameof(StatesController),
            nameof(GetPagedAsync));

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }

    [HttpGet("{id}", Name = "GetById")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] GetStateByIdQuery query)
    {
        _logger.LogGetAllStates(nameof(StatesController),
            nameof(GetPagedAsync));

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }

    [HttpGet("{id}/cities", Name = "GetCitiesById")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetCitiesByIdAsync(
        [FromRoute] GetCityByStatePagedQuery query)
    {
        _logger.GetCitiesById(nameof(StatesController),
            nameof(GetCitiesByIdAsync));

        BaseResponse response = await _mediator.Send(query);

        return StatusCode((int)response.Status, response);
    }
}
