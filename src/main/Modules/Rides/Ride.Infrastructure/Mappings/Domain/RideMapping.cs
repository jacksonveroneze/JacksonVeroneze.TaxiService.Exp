using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;

namespace TaxiService.Modules.Ride.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class RideMapping : IEntityTypeConfiguration<Ride.Domain.Aggregates.Ride>
{
    public void Configure(EntityTypeBuilder<Ride.Domain.Aggregates.Ride> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("ride");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => new { c.PassengerId, c.Status });

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Fare)
            .IsRequired(false);

        builder.Property(c => c.PassengerId)
            .IsRequired();

        builder.Property(c => c.DriverId);

        builder.Property(c => c.Distance)
            .IsRequired(false);

        builder.ComplexProperty(conf => conf.CoordinateFrom, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.ComplexProperty(conf => conf.CoordinateTo, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("to_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("to_longitude")
                .IsRequired();
        });

        builder.Property(c => c.Status)
            .IsRequired();

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
