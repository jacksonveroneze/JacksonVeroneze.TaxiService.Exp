using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Passengers.Api.Endpoints.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users.Emails;

internal static class DeleteEndpoint
{
    public static RouteGroupBuilder AddEmailDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{userId:guid:required}/emails/{emailId:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid userId,
                [FromRoute] Guid emailId,
                CancellationToken cancellationToken) =>
            {
                DeleteEmailCommand command = new(userId, emailId);

                JacksonVeroneze.NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}