using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class FinishEndpoint
{
    public static RouteGroupBuilder AddFinish(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/finish", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                FinishRideCommand command = new(id);

                JacksonVeroneze.NET.Result.Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}