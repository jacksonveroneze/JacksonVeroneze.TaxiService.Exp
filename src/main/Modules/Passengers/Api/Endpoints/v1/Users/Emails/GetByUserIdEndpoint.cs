using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints.v1.Users.Emails;

internal static class GetByUserIdEndpoint
{
    public static RouteGroupBuilder AddGetEmailsByUserId(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{userId:guid:required}/emails", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid userId,
                CancellationToken cancellationToken) =>
            {
                GetEmailPagedQuery pagedQuery = new(userId);

                NET.Result.Result<GetEmailPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetEmailPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}