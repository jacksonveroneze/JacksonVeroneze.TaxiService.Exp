using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers;

internal static class InactivatePassengerEndpoint
{
    public static RouteGroupBuilder AddInactivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/inactivate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                InactivatePassengerCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
