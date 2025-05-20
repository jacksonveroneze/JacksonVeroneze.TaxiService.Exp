using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.Reactivate;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class ReactivateDriverEndpoint
{
    public static RouteGroupBuilder AddReactivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/reactivate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ReactivateDriverCommand command = new(id);

                Result response =
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
