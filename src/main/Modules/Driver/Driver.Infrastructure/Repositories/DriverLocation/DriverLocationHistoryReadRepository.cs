using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Domain.Enums;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationHistoryReadRepository(
    IEfCoreRepository<DriverLocationDto, DriversReadDbContext> service)
    : IDriverLocationHistoryReadRepository
{
    public async Task<ICollection<DriverLocationMatchDto>> GetDriverNeabyLocationAsync(
        DriverLocationMatchFilter filter,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filter);

        GeometryFactory? geometryFactory = NtsGeometryServices
            .Instance.CreateGeometryFactory(srid: 4326);

        Point? referencePoint = geometryFactory
            .CreatePoint(new Coordinate(filter.Longitude, filter.Latitude));

        DriverLocationMatchDto[] result = await service.DbSet
            .Select(item => new DriverLocationMatchDto
            {
                Id = item.Id,
                DriverId = item.DriverId,
                Latitude = item.Location.Latitude,
                Longitude = item.Location.Longitude,
                DistanceInMeters = EF.Property<Point>(
                    item, "geoLocation").Distance(referencePoint),
            })
            .Where(item => service.DbContext.Set<DriverDto>()
                               .Any(drv => drv.Id == item.DriverId
                                           && drv.Status == DriverStatus.Active)
                           && service.DbContext.Set<VehicleDto>()
                               .Any(vei => vei.DriverId == item.DriverId)
                           && item.DistanceInMeters < filter.DistanceInMeters)
            .OrderBy(item => item.DistanceInMeters)
            .Take(filter.MaxResults)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}
