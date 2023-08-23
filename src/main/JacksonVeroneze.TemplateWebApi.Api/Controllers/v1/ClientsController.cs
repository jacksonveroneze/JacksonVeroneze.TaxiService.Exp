using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/Clients")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> _logger;
    private readonly ISender _mediator;

    public ClientsController(
        ILogger<ClientsController> logger,
        ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetPagedClients")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetClientPagedQuery query,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{id}", Name = "GetClientById")]
    //[ActionName(nameof(GetByIdAsync))]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetClientByIdQueryResponse>> GetByIdAsync(
        [FromRoute] GetClientByIdQuery query,
        CancellationToken cancellationToken)
    {
        IResult<GetClientByIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchFind(this);
    }

    [HttpPost(Name = "CreateClient")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateClientCommand command,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{id}", Name = "DeleteClient")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] DeleteClientCommand query,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchDelete(this);
    }
}
