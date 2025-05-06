using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;

namespace TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;

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