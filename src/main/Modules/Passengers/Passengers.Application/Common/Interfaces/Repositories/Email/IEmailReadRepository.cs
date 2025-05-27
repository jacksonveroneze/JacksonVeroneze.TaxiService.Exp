using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;

public interface IEmailReadRepository
{
    public Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken);

    public Task<EmailEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken);
}