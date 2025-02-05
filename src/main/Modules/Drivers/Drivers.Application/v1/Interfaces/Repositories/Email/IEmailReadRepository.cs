namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;

public interface IEmailReadRepository
{
    public Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken = default);
}