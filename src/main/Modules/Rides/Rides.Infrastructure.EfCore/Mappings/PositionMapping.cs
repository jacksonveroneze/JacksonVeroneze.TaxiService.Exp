using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class PositionMapping : IEntityTypeConfiguration<PositionEntity>
{
    public void Configure(EntityTypeBuilder<PositionEntity> builder)
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

        builder.ComplexProperty(conf => conf.Position, conf =>
        {
            conf.Property(prop => prop!.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop!.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.ConfigureDefaultFieldsMapping();
    }
}