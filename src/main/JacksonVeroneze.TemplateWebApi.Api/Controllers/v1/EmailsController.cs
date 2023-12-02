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
[Route("/api/v{version:apiVersion}/users/{userId:guid}/emails")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class EmailsController : ControllerBase
{
    private readonly ISender _mediator;

    public EmailsController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<IActionResult> GetEmailsByIdAsync(
        GetAllEmailsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetAllEmailsByUserIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateEmailAsync(
        CreateEmailCommand command,
        CancellationToken cancellationToken)
    {
        Result<CreateEmailCommandResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpDelete("{emailId:guid}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteEmailAsync(
        DeleteEmailCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchDelete(this);
    }
}
