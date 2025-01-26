using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxiService.Modules.Passengers.Api.Endpoints.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.User.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users.Emails;

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

                JacksonVeroneze.NET.Result.Result<GetEmailPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetEmailPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}