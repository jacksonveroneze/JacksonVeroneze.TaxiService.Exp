using System.Data;
using Dapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank.Dapper;

public class BankReadRepository : IBankReadRepository
{
    private readonly IDbConnection _repository;

    public BankReadRepository(
        IDbConnection connection)
    {
        _repository = connection;
    }

    public async Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        int result = await _repository
            .RecordCountAsync<BankResult>(new { name = name });

        return result > 0;
    }

    public async Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        BankResult? result = await _repository
            .GetAsync<BankResult>(id);

        if (result is null)
        {
            return null;
        }

        return new BankEntity(result.Name!)
        {
            Id = result.Id!.Value,
            Status = (BankStatus)result.Status
        };
    }

    public Task<Page<BankEntity>> GetPagedAsync(
        BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
