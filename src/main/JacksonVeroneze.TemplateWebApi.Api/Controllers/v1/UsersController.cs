using System.Net.Mime;
using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;
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
        IResult<BaseResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{userId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetUserByIdQueryResponse>> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(userId);

        IResult<GetUserByIdQueryResponse> response =
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
        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{userId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        DeleteUserCommand command = new(userId);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }

    [HttpPut("{userId:guid}/activate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateActivateAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ActivateUserCommand command = new(userId);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{userId:guid}/inactivate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateInactivateAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        InactivateUserCommand command = new(userId);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpGet("{userId:guid}/emails")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetEmailsByIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        GetAllEmailsByUserIdQuery query = new(userId);

        IResult<GetAllEmailsByUserIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpPost("{userId:guid}/emails")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateEmailAsync(
        [FromBody] CreateEmailCommand command,
        CancellationToken cancellationToken)
    {
        IResult<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{userId:guid}/emails/{emailId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteEmailAsync(
        Guid userId,
        Guid emailId,
        CancellationToken cancellationToken)
    {
        DeleteEmailCommand command = new(userId, emailId);

        IResult<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }
}
