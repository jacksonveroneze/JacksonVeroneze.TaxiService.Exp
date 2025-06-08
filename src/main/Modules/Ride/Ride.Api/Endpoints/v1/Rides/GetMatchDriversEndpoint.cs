using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.MatchDrivers;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class GetMatchDriversEndpoint
{
    public static RouteGroupBuilder AddGetMatchDrivers(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("match-drivers", async (
                [FromServices] ISender mediator,
                [AsParameters] GetMatchDriversQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetMatchDriversQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetMatchDriversQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
