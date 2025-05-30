using JacksonVeroneze.NET.EntityFramework.Interfaces;

using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;
using TaxiService.Modules.Passengers.Infrastructure.Contexts;

namespace TaxiService.Modules.Passengers.Infrastructure.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository(
    IEfCoreRepository<EmailEntity, PassengersDbContext> service)
    : IEmailReadRepository
{
    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    [SuppressMessage("Usage", "MA0011:IFormatProvider is missing")]
    [SuppressMessage("Globalization", "CA1304:Specify CultureInfo")]
    [SuppressMessage("Globalization", "CA1311:Specify a culture or use an invariant version")]
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(conf =>
                conf.Email.Value!.ToLower().Equals(email.ToLowerInvariant()),
            cancellationToken);

        return exists;
    }

    public Task<EmailEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(conf => conf.Id == id,
            cancellationToken);
    }

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return service.GetAllAsync(
            conf => conf.PassengerId == filter.PassengerId,
            cancellationToken);
    }
}
