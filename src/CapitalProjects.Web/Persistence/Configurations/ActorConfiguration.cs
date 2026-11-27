using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class ActorConfiguration
    : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actors");

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new
        {
            x.TenantId,
            x.Id
        });

        builder.Property(x => x.IdentityIssuer)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.ExternalSubjectId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.IdentityIssuer,
            x.ExternalSubjectId
        }).IsUnique();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BusinessParty)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.BusinessPartyId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Version)
            .IsRequired()
            .IsConcurrencyToken();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}