using AutoMapper;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank;

public class BankWriteRepository :
    BaseRepository<BankEntity, Guid>, IBankWriteRepository
{
    private readonly IMapper _mapper;

    public BankWriteRepository(ILogger<BankWriteRepository> logger,
        IMapper mapper,
        IDatabaseContext context) :
        base(logger, context.GetCollection<BankEntity>(nameof(BankEntity)))
    {
        _mapper = mapper;
    }

    public new Task CreateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(
            bankEntity, cancellationToken);
    }

    public new Task DeleteAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(
            bankEntity, cancellationToken);
    }

    public new Task UpdateAsync(BankEntity bankEntity,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(
            bankEntity, cancellationToken);
    }
}
