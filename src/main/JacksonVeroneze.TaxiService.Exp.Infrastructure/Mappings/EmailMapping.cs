using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class EmailMapping : IEntityTypeConfiguration<EmailEntity>
{
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("email");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.OwnsOne(conf => conf.Email, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Value)
                .HasColumnName("value")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasOne(p => p.User)
            .WithMany(b => b.Emails)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ConfigureDefaultFiledsMapping();
    }
}