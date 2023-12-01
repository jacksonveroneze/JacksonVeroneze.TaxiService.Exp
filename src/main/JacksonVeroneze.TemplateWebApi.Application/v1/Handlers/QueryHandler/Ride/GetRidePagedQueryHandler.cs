using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRidePagedQueryHandler(
    IMapper mapper,
    IRideReadRepository repository)
    : IRequestHandler<GetRidePagedQuery, IResult<GetRidePagedQueryResponse>>
{
    public async Task<IResult<GetRidePagedQueryResponse>> Handle(
        GetRidePagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RidePagedFilter filter = mapper
            .Map<RidePagedFilter>(request);

        Page<RideEntity> page = await repository
            .GetPagedAsync(filter, cancellationToken);

        GetRidePagedQueryResponse response =
            mapper.Map<GetRidePagedQueryResponse>(page);

        return Result<GetRidePagedQueryResponse>
            .Success(response);
    }
}
