using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetRideByIdQueryHandler :
    IRequestHandler<GetRideByIdQuery, IResult<GetRideByIdQueryResponse>>
{
    private readonly ILogger<GetRideByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRideReadRepository _repository;


    public GetRideByIdQueryHandler(
        ILogger<GetRideByIdQueryHandler> logger,
        IMapper mapper, IRideReadRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IResult<GetRideByIdQueryResponse>> Handle(
        GetRideByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? entity = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetRideByIdQueryResponse>
                .NotFound(DomainErrors.Ride.NotFound);
        }

        GetRideByIdQueryResponse response =
            _mapper.Map<GetRideByIdQueryResponse>(entity);

        _logger.LogGetById(nameof(GetRideByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetRideByIdQueryResponse>
            .Success(response);
    }
}
