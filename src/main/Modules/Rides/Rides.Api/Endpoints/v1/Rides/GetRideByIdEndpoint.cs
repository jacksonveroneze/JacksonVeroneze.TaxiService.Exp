using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Rides.Application.v1.Ride.GetById;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class GetRideByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetRideByIdQuery query = new(id);

                Result<GetRideByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetRideByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
