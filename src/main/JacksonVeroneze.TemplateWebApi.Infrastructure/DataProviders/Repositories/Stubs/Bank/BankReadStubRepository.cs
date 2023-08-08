using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public class BankReadStubRepository : IBankReadRepository
{
    private readonly IList<BankEntity> _data = Enumerable.Range(1, 25)
        .Select(item => new BankEntity($"Bank_{item}"))
        .ToArray();

    private readonly ILogger<BankReadStubRepository> _logger;
    private readonly IMapper _mapper;

    public BankReadStubRepository(ILogger<BankReadStubRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        BankEntity? item = _data.FirstOrDefault(item => item.Id == id);

        return Task.FromResult(item);
    }

    public Task<Page<BankEntity>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        PageInfo pageInfo = new(
            filter.Pagination!.Page,
            filter.Pagination!.PageSize,
            _data.Count);

        Page<BankEntity> paged = new(_data, pageInfo);

        return Task.FromResult(paged);
    }
}
