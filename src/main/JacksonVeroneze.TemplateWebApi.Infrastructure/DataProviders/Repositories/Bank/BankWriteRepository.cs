using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank;

public class BankWriteRepository : IBankWriteRepository
{
    private readonly IBaseRepository<BankEntity> _repository;

    public BankWriteRepository(
        IBaseRepository<BankEntity> repository)
    {
        _repository = repository;
    }

    public Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return _repository.CreateAsync(
            bankEntity, cancellationToken);
    }

    public Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync(
            bankEntity, cancellationToken);
    }

    public Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return _repository.UpdateAsync(
            bankEntity, cancellationToken);
    }
}
