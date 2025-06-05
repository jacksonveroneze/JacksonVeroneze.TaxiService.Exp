using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.Driver.Block;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Drivers;

internal static class BlockDriverEndpoint
{
    public static RouteGroupBuilder AddBlock(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/block", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request body,
                CancellationToken cancellationToken) =>
            {
                BlockDriverCommand command = new(id, body.Type);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private sealed record Request(BlockDriverCommand.BlockType Type);
}
