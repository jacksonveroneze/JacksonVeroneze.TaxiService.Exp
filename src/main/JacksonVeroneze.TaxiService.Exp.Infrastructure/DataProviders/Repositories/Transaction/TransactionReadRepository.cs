using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Specifications.Transaction;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionReadRepository :
    BaseReadRepository<TransactionEntity>, ITransactionReadRepository
{
    private readonly DbSet<TransactionEntity> _dbSet;

    public TransactionReadRepository(
        ILogger<TransactionReadRepository> logger,
        ApplicationDbContext context) : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _dbSet = context.Set<TransactionEntity>();
    }

    public Task<TransactionEntity?> GetByRideIdAsync(
        Guid rideId,
        CancellationToken cancellationToken = default)
    {
        TransactionByRideIdSpecification spec = new(rideId);

        return _dbSet
            .AsNoTrackingWithIdentityResolution()
            .Where(spec.ToExpression())
            .FirstOrDefaultAsync(cancellationToken);
    }
}