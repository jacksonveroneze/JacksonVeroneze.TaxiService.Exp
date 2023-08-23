using System.Data;
using Dapper;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Models;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Dapper;

public class BankWriteRepository : IBankWriteRepository
{
    private readonly IDbConnection _repository;

    public BankWriteRepository(
        IDbConnection connection)
    {
        _repository = connection;
    }

    public Task CreateAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        BankModel result = new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Status = (int)entity.Status
        };

        return _repository.InsertAsync<Guid, BankModel>(result);
    }

    public Task DeleteAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync<BankModel>(entity.Id);
    }

    public Task UpdateAsync(BankEntity entity,
        CancellationToken cancellationToken = default)
    {
        BankModel result = new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Status = (int)entity.Status
        };

        return _repository.UpdateAsync<BankModel>(result);
    }
}
