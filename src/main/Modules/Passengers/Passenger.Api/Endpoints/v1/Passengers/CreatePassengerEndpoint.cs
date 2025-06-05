using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Create;

namespace TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;

internal static class CreatePassengerEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreatePassengerCommand command,
                [FromServices] LinkGenerator linkGenerator,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                Result<CreatePassengerCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToCreatedResultFromRoute(
                    linkGenerator,
                    httpContext,
                    RouteNames.GetPassengerById,
                    response.Value?.Data?.Id!
                );
            })
            .Produces<CreatePassengerCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
