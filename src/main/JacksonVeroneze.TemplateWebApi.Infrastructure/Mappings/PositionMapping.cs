using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

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

        builder.OwnsOne(conf => conf.Position, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Latitude)
                .HasColumnName("from_latitude")
                .IsRequired();

            conf.Property(prop => prop.Longitude)
                .HasColumnName("from_longitude")
                .IsRequired();
        });

        builder.HasOne(p => p.Ride)
            .WithMany(b => b.Positions)
            .HasForeignKey(f => f.Id)
            .HasForeignKey("ride_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ConfigureDefaultFiledsMapping();

        builder.Ignore(x => x.Events);
    }
}
