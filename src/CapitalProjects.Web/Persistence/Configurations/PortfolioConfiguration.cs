using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class PortfolioConfiguration
    : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new
        {
            x.TenantId,
            x.Id
        });

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.Code
        }).IsUnique();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}