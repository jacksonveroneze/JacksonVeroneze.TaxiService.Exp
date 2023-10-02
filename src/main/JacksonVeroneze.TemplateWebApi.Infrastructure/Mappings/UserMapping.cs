using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class UserMapping : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(conf => conf.Name, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(c => c.Birthday)
            .HasColumnName("birthday")
            .IsRequired();

        builder.Property(c => c.Gender)
            .HasColumnName("gender")
            .IsRequired();

        builder.OwnsOne(conf => conf.Cpf, conf =>
        {
            conf.WithOwner();

            conf.Property(prop => prop.Value)
                .HasMaxLength(11)
                .HasColumnName("cpf")
                .IsRequired();
        });

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(c => c.ActivedOnUtc)
            .HasColumnName("actived_on_utc");

        builder.Property(c => c.InactivedOnUtc)
            .HasColumnName("inactived_on_utc");

        builder.ConfigureDefaultFiledsMapping();

        builder.Ignore(x => x.Events);
    }
}
