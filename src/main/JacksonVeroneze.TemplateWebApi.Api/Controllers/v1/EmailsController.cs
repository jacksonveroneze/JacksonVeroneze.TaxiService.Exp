using System.Net.Mime;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/users/{userId:guid}")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class EmailsController : ControllerBase
{
    private readonly ISender _mediator;

    public EmailsController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("emails")]
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

    [HttpPost("emails")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateEmailAsync(
        [FromBody] CreateEmailCommand command,
        CancellationToken cancellationToken)
    {
        IResult<CreateEmailCommandResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("emails/{emailId:guid}")]
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
