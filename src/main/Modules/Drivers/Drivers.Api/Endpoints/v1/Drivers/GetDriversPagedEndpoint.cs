using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Driver.GetPaged;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class GetDriversPagedEndpoint
{
    public static RouteGroupBuilder AddGetPaged(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] ISender mediator,
                [AsParameters] GetDriverPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetDriverPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.ToIResult();
            })
            .Produces<GetDriverPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
