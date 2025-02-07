using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users.Emails;

internal static class CreateEndpoint
{
    public static RouteGroupBuilder AddEmailCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{userId:guid:required}/emails", async (
                [FromServices] ISender mediator,
                [AsParameters] CreateCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}