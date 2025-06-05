using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passenger.Application.v1.Passenger.GetPaged;

namespace TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;

internal static class GetPassengersPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] ISender mediator,
                [AsParameters] GetPassengerPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetPassengerPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetPassengerPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
