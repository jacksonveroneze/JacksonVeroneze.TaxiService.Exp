using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Commands.Position;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Positions;

internal static class AddPositionEndpoint
{
    public static RouteGroupBuilder AddPosition(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] AddCommand command,
                CancellationToken cancellationToken) =>
            {
                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints()
            .Produces(StatusCodes.Status204NoContent);

        return builder;
    }
}