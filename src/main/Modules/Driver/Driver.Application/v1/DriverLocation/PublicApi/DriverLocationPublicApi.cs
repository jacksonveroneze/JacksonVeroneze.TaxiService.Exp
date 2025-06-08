using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;
using TaxiService.Modules.Driver.PublicApi.v1.Models;

namespace TaxiService.Modules.Driver.Application.v1.DriverLocation.PublicApi;

public class DriverLocationPublicApi(
    IDriverLocationHistoryReadRepository repository)
    : IDriverLocationPublicApi
{
    public async Task<IReadOnlyList<NearbyDriverDto>> GetNearbyAvailableDriversAsync(
        double latitude,
        double longitude,
        double radiusInMeters,
        int maxResults,
        CancellationToken cancellationToken)
    {
        DriverLocationMatchFilter filter = new()
        {
            Latitude = latitude,
            Longitude = longitude,
            DistanceInMeters = radiusInMeters,
            MaxResults = maxResults,
        };

        ICollection<DriverLocationMatchDto> data = await repository
            .GetDriverNeabyLocationAsync(filter, cancellationToken);

        return data.Select(item => new NearbyDriverDto
        {
            Id = item.Id,
            DriverId = item.DriverId,
            Latitude = item.Latitude,
            Longitude = item.Longitude,
            DistanceInMeters = item.DistanceInMeters,
        }).ToArray();
    }
}
