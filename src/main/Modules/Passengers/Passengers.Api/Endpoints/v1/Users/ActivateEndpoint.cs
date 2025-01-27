using TaxiService.Modules.Passengers.Application.v1.Commands.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

internal static class ActivateEndpoint
{
    public static RouteGroupBuilder AddActivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/activate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ActivateUserCommand command = new(id);

                JacksonVeroneze.NET.Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}