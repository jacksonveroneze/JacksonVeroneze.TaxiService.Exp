using Amazon.DynamoDBv2.DataModel;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.DynamoDb;

[ExcludeFromCodeCoverage]
public class UserReadRepository : IUserReadRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public UserReadRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public Task<bool> ExistsByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Page<UserEntity>> GetPagedAsync(UserPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
