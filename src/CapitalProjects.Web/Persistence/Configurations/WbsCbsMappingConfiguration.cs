using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class WbsCbsMappingConfiguration
    : IEntityTypeConfiguration<WbsCbsMapping>
{
    public void Configure(
        EntityTypeBuilder<WbsCbsMapping> builder)
    {
        builder.ToTable("WbsCbsMappings", table =>
        {
            table.HasCheckConstraint(
                "CK_WbsCbsMappings_EffectiveDates",
                "\"EffectiveToUtc\" IS NULL OR " +
                "\"EffectiveToUtc\" > \"EffectiveFromUtc\"");

            table.HasCheckConstraint(
                "CK_WbsCbsMappings_ActiveValidity",
                "(\"IsActive\" = TRUE AND " +
                "\"EffectiveToUtc\" IS NULL) OR " +
                "(\"IsActive\" = FALSE AND " +
                "\"EffectiveToUtc\" IS NOT NULL)");
        });

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => new
        {
            x.TenantId,
            x.ProjectId,
            x.Id
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.ProjectId,
            x.WbsNodeId,
            x.CbsNodeId
        })
            .IsUnique()
            .HasFilter("\"IsActive\" = TRUE");

        builder.Property(x => x.SourceUnitReference)
            .HasMaxLength(100);

        builder.Property(x => x.MeasurementReference)
            .HasMaxLength(100);

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

        builder.HasOne(x => x.WbsNode)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.WbsNodeId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CbsNode)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.CbsNodeId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Actor>()
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.CreatedByActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Actor>()
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.UpdatedByActorId
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

        builder.Property(x => x.EffectiveFromUtc)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();
    }
}