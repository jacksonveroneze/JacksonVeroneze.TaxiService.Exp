using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Positions;

internal static class AddPositionEndpoint
{
    public static RouteGroupBuilder AddPosition(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] AddPositionCommand command,
                CancellationToken cancellationToken) =>
            {
                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints()
            .Produces(StatusCodes.Status204NoContent);

        return builder;
    }
}