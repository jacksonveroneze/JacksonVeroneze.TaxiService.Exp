using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Driver.Delete;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Drivers;

internal static class DeleteDriverEndpoint
{
    public static RouteGroupBuilder AddDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                DeleteDriverCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
