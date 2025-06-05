using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Payment.Domain.Repositories;
using TaxiService.Modules.Payment.Infrastructure.Contexts;

namespace TaxiService.Modules.Payment.Infrastructure.Repositories.Payment;

[ExcludeFromCodeCoverage]
public class PaymentRepository(
    IEfCoreRepository<Domain.Aggregates.Payment, PaymentsDbContext> service)
    : IPaymentRepository
{
    public Task<ICollection<Domain.Aggregates.Payment>> GetByRideIdAsync(
        Guid rideId, CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }

    public async Task CreateAsync(Domain.Aggregates.Payment entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Domain.Aggregates.Payment entity,
        CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
}
