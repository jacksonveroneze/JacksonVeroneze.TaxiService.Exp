using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class GetDriverByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet("{id:guid:required}", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetDriverByIdQuery query = new(id);

                Result<GetDriverByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetDriverByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
