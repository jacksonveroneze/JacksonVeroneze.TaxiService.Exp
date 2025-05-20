using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger;

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
