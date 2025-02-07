using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

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