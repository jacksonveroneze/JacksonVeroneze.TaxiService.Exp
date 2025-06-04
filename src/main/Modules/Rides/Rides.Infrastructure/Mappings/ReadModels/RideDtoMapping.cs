using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaxiService.Modules.Rides.Application.v1.Ride;
using TaxiService.Modules.Rides.Application.v1.Ride.Common.Models;

namespace TaxiService.Modules.Rides.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class RideDtoMapping : IEntityTypeConfiguration<RideDto>
{
    public void Configure(EntityTypeBuilder<RideDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("ride");

        // Keys
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Id);
        builder.Property(c => c.Fare);
        builder.Property(c => c.PassengerId);
        builder.Property(c => c.DriverId);
        builder.Property(c => c.Distance);
        builder.Property(c => c.Status);

        builder.ComplexProperty(conf => conf.From, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.ComplexProperty(conf => conf.To, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });
    }
}
