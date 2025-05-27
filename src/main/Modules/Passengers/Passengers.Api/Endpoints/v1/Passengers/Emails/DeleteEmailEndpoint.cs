using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Passengers.Application.v1.Email.Delete;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers.Emails;

internal static class DeleteEmailEndpoint
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
                DeleteEmailCommand passengerCommand = new(passengerId, emailId);

                Result response = await mediator
                    .Send(passengerCommand, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
