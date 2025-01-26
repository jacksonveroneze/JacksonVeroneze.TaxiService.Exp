using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Application.v1.Commands.User;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Users;

internal static class ActivateEndpoint
{
    public static RouteGroupBuilder AddActivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/activate", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ActivateUserCommand command = new(id);

                JacksonVeroneze.NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}