using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Application.v1.Queries.Passanger.Email;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passangers.Emails;

internal static class GetByPassangerIdEndpoint
{
    public static RouteGroupBuilder AddGetEmailsByPassangerId(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{passangerId:guid:required}/emails", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid passangerId,
                CancellationToken cancellationToken) =>
            {
                GetPagedQuery pagedQuery = new(passangerId);

                Result<GetPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}