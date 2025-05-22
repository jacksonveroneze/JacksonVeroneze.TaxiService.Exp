namespace TaxiService.Modules.Drivers.PublicApi.Interfaces;

public interface IDriverService
{
    Task<bool> ExistsByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
