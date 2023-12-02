using System.Net.Mime;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/users")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetUserPagedQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetUserPagedQueryResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{userId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetUserByIdQueryResponse>> GetByIdAsync(
        GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetUserByIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchFind(this);
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        Result<CreateUserCommandResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{userId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }

    [HttpPut("{userId:guid}/activate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateActivateAsync(
        ActivateUserCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{userId:guid}/inactivate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateInactivateAsync(
        InactivateUserCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }
}
