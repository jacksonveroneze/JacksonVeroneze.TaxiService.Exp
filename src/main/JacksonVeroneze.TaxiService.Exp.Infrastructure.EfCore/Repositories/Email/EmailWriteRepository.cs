using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailWriteRepository : IEmailWriteRepository
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<EmailEntity> _dbSet;

    public EmailWriteRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(context);

        _unitOfWork = unitOfWork;
        _dbSet = context.Set<EmailEntity>();
    }

    public async Task CreateAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);
    }

    public Task DeleteAsync(EmailEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        return _unitOfWork.CommitAsync(cancellationToken);
    }
}