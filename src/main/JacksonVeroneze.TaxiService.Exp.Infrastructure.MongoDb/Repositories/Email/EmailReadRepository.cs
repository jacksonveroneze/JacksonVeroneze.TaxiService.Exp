using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Email;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository : IEmailReadRepository
{
    private readonly IMongoDbRepository<EmailEntity> _mongoDbRepository;

    public EmailReadRepository(
        IMongoDbRepository<EmailEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(EmailEntity));
    }

    public async Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        // EmailByValueSpecification specName = new(email);

        bool exists = await _mongoDbRepository.AnyAsync(conf => conf.Email == email,
            cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        return _mongoDbRepository.GetByIdAsync(
            conf => conf.Id == id, cancellationToken);
    }

    public Task<Page<EmailEntity>> GetPagedAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByUserIdSpecification specByUserId = new(filter.UserId);

        return _mongoDbRepository.GetPagedAsync(filter.Pagination!,
            specByUserId, cancellationToken);
    }
}