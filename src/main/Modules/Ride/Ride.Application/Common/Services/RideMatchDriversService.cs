using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.PublicApi.v1.Interfaces;
using TaxiService.Modules.Driver.PublicApi.v1.Models;
using TaxiService.Modules.Driver.PublicApi.v1.Queries;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services;
using TaxiService.Modules.Ride.Application.Common.Interfaces.Services.Models;

namespace TaxiService.Modules.Ride.Application.Common.Services;

public class RideMatchDriversService(
    IDriverPublicApi driverPublicApi,
    IDriverLocationPublicApi driverLocationPublicApi,
    IVehiclePublicApi vehiclePublicApi) : IRideMatchDriversService
{
    private const int MaxDistanceInMeters = 30_000;
    private const int MaxCandidates = 10;

    public async Task<ICollection<DriverMatchCandidate>> GetMatchDriversAsync(
        RideMatchFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        IReadOnlyList<NearbyDriverDto> nearbyDrivers =
            await driverLocationPublicApi.GetNearbyAvailableDriversAsync(
                filter.Latitude, filter.Longitude, MaxDistanceInMeters,
                MaxCandidates, cancellationToken);

        if (!nearbyDrivers.Any())
        {
            return [];
        }

        Guid[] driverIds = nearbyDrivers
            .Select(driver => driver.DriverId)
            .ToArray();

        IDictionary<Guid, DriverPublicApiDto> drivers =
            await GetDriversAsync(driverIds, cancellationToken);

        IDictionary<Guid, VehiclePublicApiDto> vehicles =
            await GetVehiclesByDriversAsync(driverIds, cancellationToken);

        ICollection<DriverMatchCandidate?> items = nearbyDrivers
            .Select((item, index) =>
            {
                bool hasDriver = drivers.TryGetValue(
                    item.DriverId, out DriverPublicApiDto? driver);

                bool hasVehicle = vehicles.TryGetValue(
                    item.DriverId, out VehiclePublicApiDto? vehicle);

                if (hasDriver && hasVehicle)
                {
                    return new DriverMatchCandidate
                    {
                        Driver = driver!,
                        Vehicle = vehicle!,
                        Score = index + 1,
                        DistanceInKm = item.DistanceInMeters / 1000.0,
                        EstimatedArrivalTime = TimeSpan.FromMinutes(
                            item.DistanceInMeters / 300.0),
                    };
                }

                return null;
            })
            .Where(item => item != null)
            .OrderByDescending(item => item!.Score)
            .ThenBy(item => item!.DistanceInKm)
            .ToArray();

        return items!;
    }

    private async Task<IDictionary<Guid, DriverPublicApiDto>> GetDriversAsync(
        Guid[] ids,
        CancellationToken cancellationToken)
    {
        DriverPublicApiFilter driverFilter = new()
        {
            Ids = ids,
            Status = DriverStatus.Active,
        };

        ICollection<DriverPublicApiDto> result =
            await driverPublicApi.GetAsync(
                driverFilter, cancellationToken);

        return result.ToDictionary(
            key => key.Id, value => value);
    }

    private async Task<IDictionary<Guid, VehiclePublicApiDto>> GetVehiclesByDriversAsync(
        Guid[] ids,
        CancellationToken cancellationToken)
    {
        VehiclePublicApiFilter vehicleFilter = new()
        {
            DriversIds = ids,
        };

        ICollection<VehiclePublicApiDto> result =
            await vehiclePublicApi.GetAsync(
                vehicleFilter, cancellationToken);

        return result.ToDictionary(
            key => key.DriverId, value => value);
    }
}
