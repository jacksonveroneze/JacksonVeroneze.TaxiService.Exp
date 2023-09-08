using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Contexts.EntityFramework;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories.User.EntityFramework;

[ExcludeFromCodeCoverage]
public class UserWriteRepository : IUserWriteRepository
{
    private readonly TemplateWebApiContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<UserEntity> _dbSet;

    public UserWriteRepository(
        IUnitOfWork unitOfWork,
        TemplateWebApiContext context)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _dbSet = context.Set<UserEntity>();
    }

    public async Task CreateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(
            entity, cancellationToken);

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(UserEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        await _unitOfWork.CommitAsync();
    }
}
