using TaxiService.Modules.Payments.Domain.Entities;

namespace TaxiService.Modules.Payments.Application.v1.Interfaces.Repositories.Payment;

public interface IPaymentWriteRepository
{
    public Task CreateAsync(
        PaymentEntity entity,
        CancellationToken cancellationToken);

    public Task UpdateAsync(
        PaymentEntity entity,
        CancellationToken cancellationToken);
}
