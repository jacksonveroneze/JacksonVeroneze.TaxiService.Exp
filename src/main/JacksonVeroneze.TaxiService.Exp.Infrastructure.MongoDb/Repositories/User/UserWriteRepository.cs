using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserWriteRepository : IUserWriteRepository
{
    private readonly IMongoDbRepository<UserEntity> _mongoDbRepository;

    public UserWriteRepository(
        IMongoDbRepository<UserEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(UserEntity));
    }

    public Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.CreateAsync(
            entity, cancellationToken);
    }

    public Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.DeleteAsync(
            conf => conf.Id == entity.Id, cancellationToken);
    }

    public Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.UpdateAsync(
            entity,
            conf => conf.Id == entity.Id,
            cancellationToken);
    }
}