using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Rides;

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
                NET.Result.Result<GetRidePagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetRidePagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}