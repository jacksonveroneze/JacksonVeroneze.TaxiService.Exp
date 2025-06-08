using JacksonVeroneze.NET.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Filters;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;
using TaxiService.Modules.Driver.Domain.Entities;
using TaxiService.Modules.Driver.Infrastructure.Contexts;

namespace TaxiService.Modules.Driver.Infrastructure.Repositories.DriverLocation;

[ExcludeFromCodeCoverage]
public class DriverLocationHistoryReadRepository(
    IEfCoreRepository<DriverLocationHistory, DriversDbContext> service)
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
            .Where(item => item.DistanceInMeters < filter.DistanceInMeters)
            .OrderBy(item => item.DistanceInMeters)
            .Take(filter.MaxResults)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}
