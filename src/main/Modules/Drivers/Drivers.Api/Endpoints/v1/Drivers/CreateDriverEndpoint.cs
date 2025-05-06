using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.Create;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class CreateDriverEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateDriverCommand command,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                Result<CreateDriverCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateDriverCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
