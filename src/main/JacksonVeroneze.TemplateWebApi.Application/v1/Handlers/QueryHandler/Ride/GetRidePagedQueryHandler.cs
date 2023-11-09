using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRidePagedQueryHandler :
    IRequestHandler<GetRidePagedQuery, IResult<GetRidePagedQueryResponse>>
{
    private readonly ILogger<GetRidePagedQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetRidePagedQueryHandler(
        ILogger<GetRidePagedQueryHandler> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<IResult<GetRidePagedQueryResponse>> Handle(
        GetRidePagedQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
