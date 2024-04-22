using System.Linq.Expressions;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Ride;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideReadRepository : IRideReadRepository
{
    private readonly IMongoDbRepository<RideEntity> _mongoDbRepository;

    public RideReadRepository(
        IMongoDbRepository<RideEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(RideEntity));
    }

    public async Task<bool> ExistsActiveByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        RideByUserIdSpecification specByUserId = new(userId);
        RideActivePerUserSpecification specActivePerUserRequested = new();

        Expression<Func<RideEntity, bool>> spec =
            specByUserId.ToExpression()
                .And(specActivePerUserRequested.ToExpression());

        bool exists = await _mongoDbRepository.AnyAsync(spec,
            cancellationToken);

        return exists;
    }

    public Task<RideEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        RideByIdSpecification spec = new(id);

        return _mongoDbRepository.GetByIdAsync(
            spec, cancellationToken);
    }

    public Task<Page<RideEntity>> GetPagedAsync(
        RidePagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        RideByStatusSpecification specByStatus = new(filter.Status);
        RideByUserIdSpecification specByUserId = new(filter.UserId);

        Expression<Func<RideEntity, bool>> spec =
            specByStatus.ToExpression().And(specByUserId);

        return _mongoDbRepository.GetPagedAsync(filter.Pagination!,
            spec, cancellationToken);
    }
}