using TaxiService.Modules.Drivers.Application.v1.DTOs;

namespace TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;

public interface IDriverReadRepository
{
    public Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
    
    public Task<bool> ExistsByEmailAndDocumentAsync(
        string email,
        string document,
        CancellationToken cancellationToken);
    
    public Task<DriverDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}