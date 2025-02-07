using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.User.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users.Emails;

internal static class GetByUserIdEndpoint
{
    public static RouteGroupBuilder AddGetEmailsByUserId(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{userId:guid:required}/emails", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid userId,
                CancellationToken cancellationToken) =>
            {
                GetPagedQuery pagedQuery = new(userId);

                Result<GetPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}