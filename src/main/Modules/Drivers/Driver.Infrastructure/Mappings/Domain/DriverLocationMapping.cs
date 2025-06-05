using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Driver.Domain.Entities;

namespace TaxiService.Modules.Driver.Infrastructure.Mappings.Domain;

[ExcludeFromCodeCoverage]
public class DriverLocationMapping : IEntityTypeConfiguration<DriverLocationHistory>
{
    public void Configure(EntityTypeBuilder<DriverLocationHistory> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver_location");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.DriverId);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.DriverId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Location, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("longitude")
                .IsRequired();
        });

        builder.ConfigureEntityDefaultFieldsMapping();
    }
}
