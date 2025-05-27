using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Domain.Entities;

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
    }
}
