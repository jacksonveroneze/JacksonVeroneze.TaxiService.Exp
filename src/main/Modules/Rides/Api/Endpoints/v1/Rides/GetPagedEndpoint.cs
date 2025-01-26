using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class GetPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] IMediator mediator,
                [AsParameters] GetRidePagedQuery query,
                CancellationToken cancellationToken) =>
            {
                JacksonVeroneze.NET.Result.Result<GetRidePagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetRidePagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}