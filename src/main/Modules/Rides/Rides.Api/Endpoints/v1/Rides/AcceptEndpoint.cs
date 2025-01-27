using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class AcceptEndpoint
{
    public static RouteGroupBuilder AddAccept(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/accept", async (
                [FromServices] ISender mediator,
                [AsParameters] AcceptRideCommand command,
                CancellationToken cancellationToken) =>
            {
                JacksonVeroneze.NET.Result.Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}