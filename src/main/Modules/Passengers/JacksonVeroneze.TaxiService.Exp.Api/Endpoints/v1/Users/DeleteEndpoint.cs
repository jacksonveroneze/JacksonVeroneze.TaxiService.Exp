using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users;

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

                NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchDelete();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}