using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
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
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(conf =>
                string.Equals(conf.Email.Value,
                    email.ToLower(System.Globalization.CultureInfo.CurrentCulture),
                    StringComparison.OrdinalIgnoreCase),
            cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        EmailByIdSpecification specByPassangerId = new(id);

        return service.GetByIdAsync(specByPassangerId.ToExpression(),
            cancellationToken);
    }

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        EmailByPassangerIdSpecification specByPassangerId = new(filter.PassangerId);

        return service.GetAllAsync(
            specByPassangerId,
            cancellationToken);
    }
}