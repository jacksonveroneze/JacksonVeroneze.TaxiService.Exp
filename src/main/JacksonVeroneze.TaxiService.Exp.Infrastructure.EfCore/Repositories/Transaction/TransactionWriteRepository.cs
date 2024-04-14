using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Transaction;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Transaction;

[ExcludeFromCodeCoverage]
public class TransactionWriteRepository : ITransactionWriteRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<TransactionEntity> _dbSet;

    public TransactionWriteRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(context);

        _unitOfWork = unitOfWork;
        _dbSet = context.Set<TransactionEntity>();
    }

    public async Task CreateAsync(TransactionEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}