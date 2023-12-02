using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Ride;

[ExcludeFromCodeCoverage]
public class RideWriteRepository : IRideWriteRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<RideEntity> _dbSet;

    public RideWriteRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(context);

        _unitOfWork = unitOfWork;
        _dbSet = context.Set<RideEntity>();
    }

    public async Task CreateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public Task DeleteAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        return _unitOfWork.CommitAsync(cancellationToken);
    }

    public Task UpdateAsync(RideEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        return _unitOfWork.CommitAsync(cancellationToken);
    }
}
