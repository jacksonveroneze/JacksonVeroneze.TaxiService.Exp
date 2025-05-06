using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Vehicle;

internal static class CreateEndpoint
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

                return response.MatchPost();
            })
            .Produces<CreateVehicleCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
