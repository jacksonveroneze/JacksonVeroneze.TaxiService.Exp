using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class VehicleEntityMapping : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("vehicle");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.DriverId);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.DriverId)
            .IsRequired();

        builder.Property(c => c.Model)
            .IsRequired();

        builder.Property(c => c.Brand)
            .IsRequired();

        builder.Property(c => c.Plate)
            .IsRequired();

        builder.Property(c => c.Color)
            .IsRequired();

        builder.Property(c => c.Year)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired();

        builder.ConfigureDefaultFieldsMapping();
    }
}