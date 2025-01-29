using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] CreateUserCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateUserCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateUserCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}