using System.Net.Mime;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Account;
using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Account;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/clients/{client_id}/accounts")]
[Produces(MediaTypeNames.Application.Json)]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly ISender _mediator;

    public AccountsController(
        ILogger<AccountsController> logger,
        ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetAccountPagedQuery query,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetAccountByIdQueryResponse>> GetByIdAsync(
        [FromRoute] GetAccountByIdQuery query,
        CancellationToken cancellationToken)
    {
        IResult<GetAccountByIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchFind(this);
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] DeleteAccountCommand query,
        CancellationToken cancellationToken)
    {
        IResult<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchDelete(this);
    }
}
