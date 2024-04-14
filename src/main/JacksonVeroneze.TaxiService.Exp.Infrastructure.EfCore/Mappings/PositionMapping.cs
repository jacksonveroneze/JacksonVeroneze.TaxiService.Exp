using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class PositionMapping : IEntityTypeConfiguration<PositionEntity>
{
    public void Configure(EntityTypeBuilder<PositionEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("position");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.RideId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Position, conf =>
        {
            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.ConfigureDefaultFiledsMapping();
    }
}