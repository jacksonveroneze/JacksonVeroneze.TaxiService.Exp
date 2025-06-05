using System.Linq.Expressions;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passenger.Application.v1.Email.Common.Filters;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Infrastructure.Contexts;

namespace TaxiService.Modules.Passenger.Infrastructure.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository(
    ILogger<EmailReadRepository> logger,
    IEfCoreRepository<Domain.Entities.Email, PassengersDbContext> service)
    : IEmailReadRepository
{
    [SuppressMessage("Globalization", "CA1309:Use ordinal string comparison")]
    [SuppressMessage("Usage", "MA0011:IFormatProvider is missing")]
    [SuppressMessage("Globalization", "CA1304:Specify CultureInfo")]
    [SuppressMessage("Globalization", "CA1311:Specify a culture or use an invariant version")]
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Email, bool>> spec
            = spec => spec.Value.Value!.ToLower()
                .Equals(email.ToLowerInvariant());

        bool exists = await service.AnyAsync(
            spec, cancellationToken);

        logger.LogExistsByValue(
            ModuleConstants.ContextName,
            nameof(EmailReadRepository),
            nameof(ExistsAsync),
            email, exists);

        return exists;
    }

    public async Task<Domain.Entities.Email?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        Expression<Func<Domain.Entities.Email, bool>> spec
            = entity => entity.Id == id;

        Domain.Entities.Email? result = await service.GetByIdAsync(
            spec, cancellationToken);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(EmailReadRepository),
            nameof(GetByIdAsync),
            id, result is not null);

        return result;
    }

    public Task<ICollection<Domain.Entities.Email>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        return service.GetAllAsync(
            conf => conf.PassengerId == filter.PassengerId,
            cancellationToken);
    }
}
