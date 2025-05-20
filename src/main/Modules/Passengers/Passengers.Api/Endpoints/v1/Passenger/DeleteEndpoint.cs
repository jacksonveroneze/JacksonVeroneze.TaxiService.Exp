using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger;

internal static class DeleteEndpoint
{
    public static RouteGroupBuilder AddDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                DeleteCommand command = new(id);

                Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchDelete();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
