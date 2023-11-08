using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

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
            .HasColumnName("fare");

        builder.Property(c => c.Distance)
            .HasColumnName("distance");

        builder.OwnsOne(conf => conf.From, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.OwnsOne(conf => conf.To, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Latitude)
                .HasColumnName("to_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("to_longitude")
                .IsRequired();
        });

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.HasOne(p => p.User);

        builder.HasOne(p => p.Driver);

        builder.ConfigureDefaultFiledsMapping();
    }
}
