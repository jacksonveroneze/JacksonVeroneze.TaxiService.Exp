using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Ride.Domain.Entities;

namespace TaxiService.Modules.Ride.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class PositionMapping : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("position");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.RideId);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.RideId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Coordinate, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
