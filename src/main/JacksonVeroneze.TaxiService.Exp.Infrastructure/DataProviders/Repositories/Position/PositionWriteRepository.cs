using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.DataProviders.Repositories.Position;

[ExcludeFromCodeCoverage]
public class PositionWriteRepository : IPositionWriteRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<PositionEntity> _dbSet;

    public PositionWriteRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(context);

        _unitOfWork = unitOfWork;
        _dbSet = context.Set<PositionEntity>();
    }

    public async Task CreateAsync(PositionEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}