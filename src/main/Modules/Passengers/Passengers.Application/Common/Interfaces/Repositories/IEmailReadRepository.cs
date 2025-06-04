using TaxiService.Modules.Passengers.Application.v1.Email.Common.Filters;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Filters;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;

public interface IEmailReadRepository
{
    public Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken);

    public Task<Email?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<ICollection<Email>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken);
}
