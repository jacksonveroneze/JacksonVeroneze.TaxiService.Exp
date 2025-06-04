using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Delete;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers;

internal static class DeletePassengerEndpoint
{
    public static RouteGroupBuilder AddDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                DeletePassengerCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
