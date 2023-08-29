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
public class UsersController : ControllerBase
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

    [HttpGet("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetUserByIdQueryResponse>> GetByIdAsync(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(id);

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

    [HttpDelete("{id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        DeleteUserCommand command = new(id);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }

    [HttpPut("{id:guid}/activate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateActivateAsync(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        ActivateUserCommand command = new(id);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{id:guid}/inactivate")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Update))]
    public async Task<IActionResult> UpdateInactivateAsync(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        InactivateUserCommand command = new(id);

        IResult<BaseResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpGet("{id:guid}/emails")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetEmailsByIdAsync(
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        GetAllEmailsByUserIdQuery query = new(id);

        IResult<GetAllEmailsByUserIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpPost("{id:guid}/emails")]
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

    [HttpPost("{id:guid}/emails/{email_id:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteEmailAsync(
        [FromRoute(Name = "id")] Guid id,
        [FromRoute(Name = "email_id")] Guid emailId,
        CancellationToken cancellationToken)
    {
        DeleteEmailCommand command = new(id, emailId);

        IResult<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }
}
