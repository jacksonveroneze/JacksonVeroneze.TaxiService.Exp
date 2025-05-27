using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;

namespace TaxiService.Modules.Passengers.Infrastructure.Mappings.ReadModels;

[ExcludeFromCodeCoverage]
public class PassengerDtoMapping : IEntityTypeConfiguration<PassengerDto>
{
    public void Configure(EntityTypeBuilder<PassengerDto> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("passenger");

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

        builder.Property(c => c.Document)
            .IsRequired()
            .HasColumnName("cpf");

        builder.Property(c => c.Gender)
            .IsRequired()
            .HasColumnName("gender");

        builder.Property(c => c.Status)
            .IsRequired()
            .HasColumnName("status");

        builder.Property(c => c.DeletedAt);
    }
}
