using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class EmailMapping : IEntityTypeConfiguration<EmailEntity>
{
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("email");

        // Keys
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.UserId);
        // builder.HasIndex(c => c.Email);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.ComplexProperty(conf => conf.Email, conf =>
        {
            conf.Property(prop => prop.Value)
                .HasColumnName("value")
                .HasMaxLength(100);
        });

        builder.ConfigureDefaultFieldsMapping();
    }
}