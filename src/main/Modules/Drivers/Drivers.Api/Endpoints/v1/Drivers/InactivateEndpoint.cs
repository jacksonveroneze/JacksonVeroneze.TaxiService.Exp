using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class InactivateEndpoint
{
    public static RouteGroupBuilder AddInactivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/inactivate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                InactivateCommand command = new(id);

                Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}