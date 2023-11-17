using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.Ride;

public sealed class GetRideService : IGetRideService
{
    private readonly ILogger<GetRideService> _logger;
    private readonly IRideReadRepository _readRepository;

    public GetRideService(
        ILogger<GetRideService> logger,
        IRideReadRepository readRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
    }

    public async Task<IResult<RideEntity>> TryGetRideAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        RideEntity? entity = await _readRepository
            .GetByIdAsync(id, cancellationToken);

        return entity is not null
            ? Result<RideEntity>.Success(entity)
            : Result<RideEntity>.NotFound(DomainErrors.Ride.NotFound);
    }
}
