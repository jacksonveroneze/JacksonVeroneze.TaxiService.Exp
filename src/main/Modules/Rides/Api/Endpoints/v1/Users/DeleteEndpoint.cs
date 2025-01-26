using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Application.v1.Commands.User;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Users;

internal static class DeleteEndpoint
{
    public static RouteGroupBuilder AddDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                DeleteUserCommand command = new(id);

                JacksonVeroneze.NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchDelete();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}