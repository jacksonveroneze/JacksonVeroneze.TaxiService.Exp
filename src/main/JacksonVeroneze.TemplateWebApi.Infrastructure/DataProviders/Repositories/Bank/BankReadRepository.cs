using AutoMapper;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Specifications;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.Bank;

public class BankReadRepository :
    BaseRepository<BankEntity, Guid>, IBankReadRepository
{
    private readonly IMapper _mapper;

    public BankReadRepository(ILogger<BankReadRepository> logger,
        IMapper mapper,
        IDatabaseContext context) :
        base(logger, context.GetCollection<BankEntity>(nameof(BankEntity)))
    {
        _mapper = mapper;
    }

    public Task<bool> AnyByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        return AnyAsync(x => x.Name.Equals(name,
            StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    public new Task<BankEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    public new Task<Page<BankEntity>> GetPagedAsync(BankPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        BankNameSpecification specName = new(filter.Name);
        //BankStatusSpecification specStatus = new(filter);

        //Specification<BankEntity>? spec = specName.And(specStatus);

        return base.GetPagedAsync(
            filter.Pagination!, specName, cancellationToken);
    }
}
