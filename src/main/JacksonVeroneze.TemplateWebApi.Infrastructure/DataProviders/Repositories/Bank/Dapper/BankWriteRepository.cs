using System.Data;
using Dapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Results;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Dapper;

public class BankWriteRepository : IBankWriteRepository
{
    private readonly IDbConnection _repository;

    public BankWriteRepository(
        IDbConnection connection)
    {
        _repository = connection;
    }

    public Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        BankResult result = new()
        {
            Id = bankEntity.Id,
            Name = bankEntity.Name,
            Status = (int)bankEntity.Status
        };

        return _repository.InsertAsync<Guid, BankResult>(result);
    }

    public Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync<BankResult>(bankEntity.Id);
    }

    public Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        BankResult result = new()
        {
            Id = bankEntity.Id,
            Name = bankEntity.Name,
            Status = (int)bankEntity.Status
        };

        return _repository.UpdateAsync<BankResult>(result);
    }
}
