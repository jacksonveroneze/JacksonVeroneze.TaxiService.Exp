using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Drivers.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class VehicleDtoMapping : IEntityTypeConfiguration<VehicleDto>
{
    public void Configure(EntityTypeBuilder<VehicleDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("vehicle");

        // Keys
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Id);
        builder.Property(c => c.DriverId);
        builder.Property(c => c.Model);
        builder.Property(c => c.Brand);
        builder.Property(c => c.Plate);
        builder.Property(c => c.Color);
        builder.Property(c => c.Year);
        builder.Property(c => c.Status);
        builder.Property(c => c.DeletedAt);
    }
}
