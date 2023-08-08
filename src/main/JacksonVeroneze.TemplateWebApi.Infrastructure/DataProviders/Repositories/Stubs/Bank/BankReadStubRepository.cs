using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Stubs.Bank;

public class BankReadStubRepository : IBankReadRepository
{
    private readonly IList<Domain.Entities.Bank> _data = Enumerable.Range(1, 25)
        .Select(item => new Domain.Entities.Bank($"Bank_{item}"))
        .ToArray();

    private readonly ILogger<BankReadStubRepository> _logger;
    private readonly IMapper _mapper;

    public BankReadStubRepository(ILogger<BankReadStubRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Task<Domain.Entities.Bank?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        Domain.Entities.Bank? item = _data.FirstOrDefault(item => item.Id == id);

        return Task.FromResult(item);
    }

    public Task<Page<Domain.Entities.Bank>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        PageInfo pageInfo = new(
            filter.Pagination!.Page,
            filter.Pagination!.PageSize,
            _data.Count);

        Page<Domain.Entities.Bank> paged = new(_data, pageInfo);

        return Task.FromResult(paged);
    }
}
