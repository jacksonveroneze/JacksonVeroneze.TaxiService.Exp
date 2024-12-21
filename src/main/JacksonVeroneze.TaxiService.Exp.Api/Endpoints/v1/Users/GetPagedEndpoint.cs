using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result = JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users;

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