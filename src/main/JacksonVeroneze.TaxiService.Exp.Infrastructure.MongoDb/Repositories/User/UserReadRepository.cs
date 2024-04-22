using System.Linq.Expressions;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Base.Predicate;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.User;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserReadRepository : IUserReadRepository
{
    private readonly IMongoDbRepository<UserEntity> _mongoDbRepository;

    public UserReadRepository(
        IMongoDbRepository<UserEntity> mongoDbRepository)
    {
        _mongoDbRepository = mongoDbRepository;

        _mongoDbRepository.DefineCollection(nameof(UserEntity));
    }

    public Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        UserByIdSpecification spec = new(id);

        return _mongoDbRepository.GetByIdAsync(
            spec, cancellationToken);
    }

    public Task<Page<UserEntity>> GetPagedAsync(
        UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        UserByNameSpecification specByName = new(filter.Name);
        UserByStatusSpecification specByStatus = new(filter.Status);

        Expression<Func<UserEntity, bool>> spec =
            specByName.ToExpression().And(specByStatus);

        return _mongoDbRepository.GetPagedAsync(filter.Pagination!,
            spec, cancellationToken);
    }
}