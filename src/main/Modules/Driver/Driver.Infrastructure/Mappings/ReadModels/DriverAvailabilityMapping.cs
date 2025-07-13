using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Common.Models;

namespace TaxiService.Modules.Driver.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class DriverAvailabilityDtoMapping : IEntityTypeConfiguration<DriverAvailabilityDto>
{
    public void Configure(EntityTypeBuilder<DriverAvailabilityDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver_availability");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.DriverId);

        // Properties
        builder.Property(c => c.Id);
        builder.Property(c => c.DriverId);
        builder.Property(c => c.Status);
        builder.Property(c => c.CreatedAt);
        builder.Property(c => c.DeletedAt);
    }
}
