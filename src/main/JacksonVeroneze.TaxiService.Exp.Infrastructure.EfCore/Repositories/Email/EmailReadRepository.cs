using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Email;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository :
    BaseReadRepository<EmailEntity>, IEmailReadRepository
{
    private readonly DbSet<EmailEntity> _dbSet;

    public EmailReadRepository(
        ILogger<EmailReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<EmailEntity>();
    }

    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken = default)
    {
        EmailByValueSpecification specName = new(email);

        bool exists = await _dbSet.AnyAsync(specName,
            cancellationToken);

        return exists;
    }

    public Task<Page<EmailEntity>> GetPagedAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByUserIdSpecification specByUserId = new(filter.UserId);

        return GetPagedAsync(specByUserId, filter.Pagination!,
            cancellationToken);
    }
}