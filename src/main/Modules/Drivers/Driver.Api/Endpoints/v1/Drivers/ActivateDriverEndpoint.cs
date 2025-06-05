using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Driver.Activate;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Drivers;

internal static class ActivateDriverEndpoint
{
    public static RouteGroupBuilder AddActivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/activate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ActivateDriverCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
