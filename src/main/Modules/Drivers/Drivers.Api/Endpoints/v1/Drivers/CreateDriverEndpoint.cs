using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.Create;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class CreateDriverEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateDriverCommand command,
                [FromServices] LinkGenerator linkGenerator,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                Result<CreateDriverCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToCreatedResultFromRoute(
                    linkGenerator,
                    httpContext,
                    RouteNames.GetDriverById,
                    response.Value?.Data?.Id!
                );
            })
            .Produces<CreateDriverCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
