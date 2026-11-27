using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class BusinessPartyConfiguration
    : IEntityTypeConfiguration<BusinessParty>
{
    public void Configure(EntityTypeBuilder<BusinessParty> builder)
    {
        builder.ToTable("BusinessParties");

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new
        {
            x.TenantId,
            x.Id
        });

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.LegalName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.PartyType)
            .IsRequired()
            .HasMaxLength(32);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.Code
        }).IsUnique();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
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