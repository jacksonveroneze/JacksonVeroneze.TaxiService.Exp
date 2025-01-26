
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Rides;

internal static class CancelEndpoint
{
    public static RouteGroupBuilder AddCancel(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/cancel", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                CancelRideCommand command = new(id);

                NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}