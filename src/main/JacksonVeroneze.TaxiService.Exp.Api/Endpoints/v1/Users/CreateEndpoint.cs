using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] CreateUserCommand command,
                CancellationToken cancellationToken) =>
            {
                NET.Result.Result<CreateUserCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .RequireAuthorization(AuthorizationExtensions.UserWrite)
            .Produces<CreateUserCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}