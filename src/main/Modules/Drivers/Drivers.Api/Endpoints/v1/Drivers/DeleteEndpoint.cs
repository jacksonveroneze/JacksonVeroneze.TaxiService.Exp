using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.Delete;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

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