using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Passengers.Api.Endpoints.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

internal static class InactivateEndpoint
{
    public static RouteGroupBuilder AddInactivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/inactivate", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                InactivateUserCommand command = new(id);

                JacksonVeroneze.NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}