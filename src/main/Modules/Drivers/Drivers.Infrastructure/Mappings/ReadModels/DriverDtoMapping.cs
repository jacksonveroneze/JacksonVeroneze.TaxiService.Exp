using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Modules.Drivers.Application.v1.Driver;

namespace TaxiService.Modules.Drivers.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class DriverDtoMapping : IEntityTypeConfiguration<DriverDto>
{
    public void Configure(EntityTypeBuilder<DriverDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("driver");

        // Keys
        builder.HasKey(c => c.Id);

        // Properties
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(c => c.FullName)
            .IsRequired()
            .HasColumnName("name");
        
        builder.Property(c => c.Birthday)
            .IsRequired()
            .HasColumnName("birthday");
        
        builder.Property(c => c.Email)
            .IsRequired()
            .HasColumnName("email");
        
        builder.Property(c => c.Document)
            .IsRequired()
            .HasColumnName("cpf");

        builder.Property(c => c.Gender)
            .IsRequired()
            .HasColumnName("gender");

        builder.Property(c => c.Status)
            .IsRequired()
            .HasColumnName("status");
    }
}