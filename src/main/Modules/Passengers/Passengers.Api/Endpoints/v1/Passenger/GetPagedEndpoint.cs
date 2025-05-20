using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger;

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
