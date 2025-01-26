using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Filters;

namespace TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;

public interface IEmailReadRepository
{
    public Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken = default);

    public Task<EmailEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<ICollection<EmailEntity>> GetAllAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default);
}