using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Vehicle.GetById;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Vehicles;

internal static class GetVehicleByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetVehicleByIdQuery query = new(id);

                Result<GetVehicleByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetVehicleByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
