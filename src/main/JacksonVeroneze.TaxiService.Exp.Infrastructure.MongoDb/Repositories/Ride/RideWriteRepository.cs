using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideWriteRepository : IRideWriteRepository
{
    private readonly IMongoDbRepository<RideEntity> _mongoDbRepository;

    public RideWriteRepository(
        IMongoDbRepository<RideEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(RideEntity));
    }

    public Task CreateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        return _mongoDbRepository.CreateAsync(
            entity, cancellationToken);
    }

    public Task UpdateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.UpdateAsync(
            entity,
            conf => conf.Id == entity.Id,
            cancellationToken);
    }
}