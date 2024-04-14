using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.User;

[ExcludeFromCodeCoverage]
public class UserWriteRepository : IUserWriteRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<UserEntity> _dbSet;

    public UserWriteRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(context);

        _unitOfWork = unitOfWork;
        _dbSet = context.Set<UserEntity>();
    }

    public async Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        return _unitOfWork.CommitAsync(cancellationToken);
    }

    public Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        return _unitOfWork.CommitAsync(cancellationToken);
    }
}