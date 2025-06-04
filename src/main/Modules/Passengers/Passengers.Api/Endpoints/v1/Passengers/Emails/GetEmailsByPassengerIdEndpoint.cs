using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Email.GetPaged;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Passengers.Emails;

internal static class GetEmailsByPassengerIdEndpoint
{
    public static RouteGroupBuilder AddGetEmailsByPassengerId(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{passengerId:guid:required}/emails", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid passengerId,
                CancellationToken cancellationToken) =>
            {
                GetEmailPagedQuery pagedQuery = new(passengerId);

                Result<GetEmailPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetEmailPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
