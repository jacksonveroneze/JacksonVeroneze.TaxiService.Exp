using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityMapping : IEntityTypeConfiguration<DriverAvailabilityEntity>
{
    public void Configure(EntityTypeBuilder<DriverAvailabilityEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver_availability");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.DriverId);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.DriverId)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.ConfigureDefaultFieldsMapping();
    }
}