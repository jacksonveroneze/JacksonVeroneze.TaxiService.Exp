using TaxiService.Modules.Driver.PublicApi.v1.Models;

namespace TaxiService.Modules.Driver.PublicApi.v1.Interfaces;

public interface IDriverLocationPublicApi
{
    Task<IReadOnlyList<NearbyDriverDto>> GetNearbyAvailableDriversAsync(
        double latitude,
        double longitude,
        double radiusInMeters,
        int maxResults,
        CancellationToken cancellationToken);
}
