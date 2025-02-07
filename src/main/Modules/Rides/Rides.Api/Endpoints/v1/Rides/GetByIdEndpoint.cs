using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class GetByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetByIdQuery query = new(id);

                Result<GetByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}