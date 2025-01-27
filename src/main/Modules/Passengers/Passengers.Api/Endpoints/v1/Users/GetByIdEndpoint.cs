using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Application.v1.Queries.User;

namespace TaxiService.Modules.Passengers.Api.Endpoints.v1.Users;

internal static class GetByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetUserByIdQuery query = new(id);

                JacksonVeroneze.NET.Result.Result<GetUserByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetUserByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}