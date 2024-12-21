using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Emails;

internal static class DeleteEndpoint
{
    public static RouteGroupBuilder AddDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{id:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                [FromRoute] Guid emailId,
                CancellationToken cancellationToken) =>
            {
                DeleteEmailCommand command = new(id, emailId);

                NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}