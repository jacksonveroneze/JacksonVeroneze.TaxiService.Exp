using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionWriteRepository : IPositionWriteRepository
{
    private readonly IMongoDbRepository<PositionEntity> _mongoDbRepository;

    public PositionWriteRepository(
        IMongoDbRepository<PositionEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(PositionEntity));
    }

    public Task CreateAsync(PositionEntity entity,
        CancellationToken cancellationToken = default)
    {
        return _mongoDbRepository.CreateAsync(
            entity, cancellationToken);
    }
}