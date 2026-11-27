using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class PackageConfiguration
    : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.ToTable("Packages");

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new
        {
            x.TenantId,
            x.ProjectId,
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
            x.ProjectId,
            x.Code
        }).IsUnique();

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId
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