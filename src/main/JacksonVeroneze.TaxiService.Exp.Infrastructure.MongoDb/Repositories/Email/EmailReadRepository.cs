using System.Linq.Expressions;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Email;
using MongoDB.Driver;

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
        ArgumentException.ThrowIfNullOrEmpty(email);

        //EmailByValueSpecification specName = new(email);

        FilterDefinition<EmailEntity>? filtro =
            Builders<EmailEntity>.Filter.Where(p => p.Email! == email);

        Expression<Func<EmailEntity, bool>> filtroExpression = p => p.Email == email;

        bool exists = await _mongoDbRepository
            .AnyAsync(filtroExpression, cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        EmailByIdSpecification spec = new(id);

        return _mongoDbRepository.GetByIdAsync(
            spec, cancellationToken);
    }

    public async Task<Page<EmailEntity>> GetPagedAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByUserIdSpecification specByUserId = new(filter.UserId);

        return await _mongoDbRepository.GetPagedAsync(filter.Pagination!,
            specByUserId, cancellationToken);
    }
}