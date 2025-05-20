using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Commands.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passenger.Emails;

internal static class DeleteEndpoint
{
    public static RouteGroupBuilder AddEmailDelete(
        this RouteGroupBuilder builder)
    {
        builder.MapDelete("{passengerId:guid:required}/emails/{emailId:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid passengerId,
                [FromRoute] Guid emailId,
                CancellationToken cancellationToken) =>
            {
                DeleteCommand command = new(passengerId, emailId);

                Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
