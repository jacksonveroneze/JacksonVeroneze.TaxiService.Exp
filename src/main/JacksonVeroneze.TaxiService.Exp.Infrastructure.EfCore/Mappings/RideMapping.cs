using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Mappings;

[ExcludeFromCodeCoverage]
public class RideMapping : IEntityTypeConfiguration<RideEntity>
{
    public void Configure(EntityTypeBuilder<RideEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("ride");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Fare)
            .IsRequired(false);

        builder.Property(c => c.UserId)
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

        builder.ConfigureDefaultFiledsMapping();
    }
}