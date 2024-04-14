using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository : IEmailWriteRepository
{
    private readonly IMongoDbRepository<EmailEntity> _mongoDbRepository;

    public EmailWriteRepository(
        IMongoDbRepository<EmailEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(EmailEntity));
    }

    public Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.CreateAsync(
            entity, cancellationToken);
    }

    public Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.DeleteAsync(
            conf => conf.Id == entity.Id, cancellationToken);
    }

    public Task UpdateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return _mongoDbRepository.UpdateAsync(
            entity,
            conf => conf.Id == entity.Id,
            cancellationToken);
    }
}