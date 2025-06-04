using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Passenger.GetById;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers;

internal static class GetPassengerByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetPassengerByIdQuery query = new(id);

                Result<GetPassengerByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetPassengerByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
