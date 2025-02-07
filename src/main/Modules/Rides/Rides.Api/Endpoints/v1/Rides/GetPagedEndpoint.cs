using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class GetPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] ISender mediator,
                [AsParameters] GetPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}