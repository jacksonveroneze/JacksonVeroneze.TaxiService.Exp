using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Create;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Vehicles;

internal static class CreateVehicleEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateVehicleCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateVehicleCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces<CreateVehicleCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
