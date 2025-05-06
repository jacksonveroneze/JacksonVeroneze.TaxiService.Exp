using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.GetById;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Vehicle;

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
                GetByIdQuery query = new(id);

                Result<GetByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}