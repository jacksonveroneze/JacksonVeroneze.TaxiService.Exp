using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers;

internal static class CreatePassengerEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateCommandResponse> response = 
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces<CreateCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
