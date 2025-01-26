using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Passengers.Api.Endpoints.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Ride;
using TaxiService.Modules.Passengers.Application.v1.Models.Ride;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Rides;

internal static class RequestEndpoint
{
    public static RouteGroupBuilder AddRequest(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] RequestRideCommand command,
                CancellationToken cancellationToken) =>
            {
                JacksonVeroneze.NET.Result.Result<RequestRideCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<RequestRideCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}