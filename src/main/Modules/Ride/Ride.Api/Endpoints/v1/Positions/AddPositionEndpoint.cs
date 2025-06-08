using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Position.Add;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Positions;

internal static class AddPositionEndpoint
{
    public static RouteGroupBuilder AddPosition(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] AddPositionCommand command,
                CancellationToken cancellationToken) =>
            {
                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .AddDefaultResponseEndpoints()
            .Produces(StatusCodes.Status204NoContent);

        return builder;
    }
}
