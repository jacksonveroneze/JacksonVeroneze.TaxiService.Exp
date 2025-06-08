using TaxiService.Modules.Passenger.Application.v1.Email.Common.Filters;
using TaxiService.Modules.Passenger.Domain.Entities;

namespace TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;

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
