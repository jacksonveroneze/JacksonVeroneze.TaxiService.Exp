using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Rides.Domain.Entities;
using TaxiService.Modules.Rides.Domain.Filters;
using TaxiService.Modules.Rides.Domain.Specifications.Email;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository(
    IEfCoreRepository<EmailEntity> service)
    : IEmailReadRepository
{
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken = default)
    {
        bool exists = await service.AnyAsync(conf =>
                string.Equals(conf.Email.Value, email.ToLower()),
            cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        EmailByIdSpecification specByUserId = new(id);

        return service.GetByIdAsync(specByUserId.ToExpression(),
            cancellationToken);
    }

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByUserIdSpecification specByUserId = new(filter.UserId);

        return service.GetAllAsync(
            specByUserId,
            cancellationToken);
    }
}