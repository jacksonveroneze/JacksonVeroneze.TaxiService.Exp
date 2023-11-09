using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRideByIdQueryHandler :
    IRequestHandler<GetRideByIdQuery, IResult<GetRideByIdQueryResponse>>
{
    private readonly ILogger<GetRideByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetRideByIdQueryHandler(
        ILogger<GetRideByIdQueryHandler> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<IResult<GetRideByIdQueryResponse>> Handle(
        GetRideByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
