using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class AcceptEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddAccept(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/accept", async (
                [FromServices] ISender mediator,
                [AsParameters] AcceptCommand command,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                Result response = await mediator
                    .Send(command, cancellationToken);

                using Activity? activity1 = ActivitySource.StartActivity("MatchPut");

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}