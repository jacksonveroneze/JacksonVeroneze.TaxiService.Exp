using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.GetPaged;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class GetPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] ISender mediator,
                [AsParameters] GetRidePagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetRidePagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetRidePagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
