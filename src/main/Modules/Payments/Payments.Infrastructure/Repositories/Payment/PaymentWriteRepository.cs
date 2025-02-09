using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;
using TaxiService.Modules.Payments.Domain.Entities;
using TaxiService.Modules.Payments.Infrastructure.Contexts;

namespace TaxiService.Modules.Payments.Infrastructure.Repositories.Payment;

[ExcludeFromCodeCoverage]
public class PaymentWriteRepository(
    IEfCoreRepository<PaymentEntity, PaymentsDbContext> service)
    : IPaymentWriteRepository
{
    public async Task CreateAsync(PaymentEntity entity,
        CancellationToken cancellationToken)
    {
        await service.CreateAsync(entity, cancellationToken);

        await service.DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(PaymentEntity entity, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}