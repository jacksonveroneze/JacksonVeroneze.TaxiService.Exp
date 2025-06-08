using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Inactivate;

namespace TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;

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
