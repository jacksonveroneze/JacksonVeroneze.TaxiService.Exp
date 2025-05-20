using System.Diagnostics;

using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger;

internal static class CreateEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateCommand command,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                Result<CreateCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
