using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxiService.Modules.Passengers.Application.v1.Passenger.Common.Models;

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
        builder.Property(c => c.Id);
        builder.Property(c => c.FullName).HasColumnName("name");
        builder.Property(c => c.Birthday);
        builder.Property(c => c.Document).HasColumnName("cpf");
        builder.Property(c => c.Gender);
        builder.Property(c => c.Status);
        builder.Property(c => c.DeletedAt);
    }
}
