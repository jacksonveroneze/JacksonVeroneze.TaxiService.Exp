using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers;

internal static class CreatePassengerEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreatePassengerCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreatePassengerCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces<CreatePassengerCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
