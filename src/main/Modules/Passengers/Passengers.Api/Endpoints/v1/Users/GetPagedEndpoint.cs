using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Common.BaseApi.Endpoints.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;
using Result = JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

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
            .Produces<GetUserPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}