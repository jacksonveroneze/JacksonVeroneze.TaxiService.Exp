using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Domain.Specifications.Email;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository(
    IEfCoreRepository<EmailEntity, PassengersDbContext> service)
    : IEmailReadRepository
{
    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(conf =>
                conf.Email.Value!.Equals(email.ToLowerInvariant()),
            cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        EmailByIdSpecification specByPassengerId = new(id);

        return service.GetByIdAsync(specByPassengerId.ToExpression(),
            cancellationToken);
    }

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByPassengerIdSpecification specByPassengerId = new(filter.PassengerId);

        return service.GetAllAsync(
            specByPassengerId,
            cancellationToken);
    }
}
