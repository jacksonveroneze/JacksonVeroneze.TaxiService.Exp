using Amazon.DynamoDBv2.DataModel;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.DynamoDb;

public class BankWriteRepository : IBankWriteRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public BankWriteRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
