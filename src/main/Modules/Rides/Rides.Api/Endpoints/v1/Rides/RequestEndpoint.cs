using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Commands.Ride;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class RequestEndpoint
{
    public static RouteGroupBuilder AddRequest(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] RequestCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<RequestCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<RequestCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}