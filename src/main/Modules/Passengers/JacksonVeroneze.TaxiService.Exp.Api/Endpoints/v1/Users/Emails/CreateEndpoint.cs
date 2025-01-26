using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users.Emails;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder AddEmailCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{userId:guid:required}/emails", async (
                [FromServices] IMediator mediator,
                [AsParameters] CreateEmailCommand command,
                CancellationToken cancellationToken) =>
            {
                NET.Result.Result<CreateEmailCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateEmailCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}