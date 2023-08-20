using Amazon.DynamoDBv2.DataModel;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.DynamoDb;

public class BankReadRepository : IBankReadRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public BankReadRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Page<BankEntity>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
