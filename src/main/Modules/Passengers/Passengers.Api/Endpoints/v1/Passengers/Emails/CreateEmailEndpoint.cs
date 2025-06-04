using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Email.Create;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers.Emails;

internal static class CreateEmailEndpoint
{
    public static RouteGroupBuilder AddEmailCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{passengerId:guid:required}/emails", async (
                [FromServices] ISender mediator,
                [AsParameters] CreateEmailCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateEmailCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces<CreateEmailCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
