using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Rides.Api.Endpoints.Extensions;
using TaxiService.Modules.Rides.Application.v1.Models.User;
using TaxiService.Modules.Rides.Application.v1.Queries.User;
using TaxiService.Modules.Rides.Infrastructure.Extensions;
using Result = JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Users;

internal static class GetPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] IMediator mediator,
                [AsParameters] GetUserPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result.Result<GetUserPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .RequireAuthorization(AuthorizationExtensions.UserRead)
            .Produces<GetUserPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}