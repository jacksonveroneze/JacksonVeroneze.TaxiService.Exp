using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.Vehicle.Create;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Vehicle;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
