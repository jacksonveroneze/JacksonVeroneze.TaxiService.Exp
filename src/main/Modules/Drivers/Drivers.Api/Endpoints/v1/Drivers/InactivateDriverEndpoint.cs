using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.Driver.Inactivate;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class InactivateDriverEndpoint
{
    public static RouteGroupBuilder AddInactivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/inactivate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                InactivateDriverCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
