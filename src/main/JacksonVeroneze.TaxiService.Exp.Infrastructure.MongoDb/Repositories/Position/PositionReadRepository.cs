using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Position;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionReadRepository : IPositionReadRepository
{
    private readonly IMongoDbRepository<PositionEntity> _mongoDbRepository;

    public PositionReadRepository(
        IMongoDbRepository<PositionEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(PositionEntity));
    }

    public Task<ICollection<PositionEntity>> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        PositionByRideIdSpecification spec = new(rideId);

        return _mongoDbRepository.GetAllAsync(
            spec, cancellationToken);
    }
}