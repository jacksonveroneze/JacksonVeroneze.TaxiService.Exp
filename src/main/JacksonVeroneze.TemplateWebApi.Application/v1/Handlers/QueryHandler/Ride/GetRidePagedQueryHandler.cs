using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRidePagedQueryHandler :
    IRequestHandler<GetRidePagedQuery, IResult<GetRidePagedQueryResponse>>
{
    private readonly ILogger<GetRidePagedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRideReadRepository _repository;


    public GetRidePagedQueryHandler(
        ILogger<GetRidePagedQueryHandler> logger,
        IMapper mapper, IRideReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetRidePagedQueryResponse>> Handle(
        GetRidePagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RidePagedFilter filter = _mapper
            .Map<RidePagedFilter>(request);

        Page<RideEntity> page = await _repository
            .GetPagedAsync(filter, cancellationToken);

        GetRidePagedQueryResponse response =
            _mapper.Map<GetRidePagedQueryResponse>(page);

        return Result<GetRidePagedQueryResponse>
            .Success(response);
    }
}
