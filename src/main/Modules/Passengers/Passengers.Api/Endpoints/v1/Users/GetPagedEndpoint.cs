using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

internal static class GetPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] ISender mediator,
                [AsParameters] GetPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}