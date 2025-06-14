using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
using TaxiService.Modules.Driver.Application.v1.DriverLocation.Common.Models;

namespace TaxiService.Modules.Driver.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class DriverLocationDtoMapping : IEntityTypeConfiguration<DriverLocationDto>
{
    public void Configure(EntityTypeBuilder<DriverLocationDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver_location");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.DriverId);

        // Properties
        builder.Property(c => c.Id);
        builder.Property(c => c.DriverId);
        builder.Property(c => c.DeletedAt);

        builder.ComplexProperty(conf => conf.Location, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("longitude")
                .IsRequired();
        });
        
        builder
            .Property<Point>("geoLocation")
            .HasColumnType("geography (point, 4326)")
            .HasComputedColumnSql(
                "ST_SetSRID(ST_MakePoint(\"longitude\", \"latitude\"), 4326)",
                stored: true);
    }
}
