using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class AuditRecordConfiguration
    : IEntityTypeConfiguration<AuditRecord>
{
    public void Configure(
        EntityTypeBuilder<AuditRecord> builder)
    {
        builder.ToTable("AuditRecords", table =>
        {
            table.HasCheckConstraint(
                "CK_AuditRecords_Context",
                "(\"ProjectId\" IS NULL OR " +
                "\"TenantId\" IS NOT NULL) AND " +
                "(\"PackageId\" IS NULL OR " +
                "(\"TenantId\" IS NOT NULL AND " +
                "\"ProjectId\" IS NOT NULL)) AND " +
                "(\"ActorId\" IS NULL OR " +
                "\"TenantId\" IS NOT NULL)");

            table.HasCheckConstraint(
                "CK_AuditRecords_DetailsObject",
                "\"DetailsJson\" IS NULL OR " +
                "jsonb_typeof(\"DetailsJson\") = 'object'");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ResourceType)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Outcome)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.ReasonCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DetailsJson)
            .HasColumnType("jsonb");

        builder.Property(x => x.CorrelationId)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.OccurredAtUtc)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.ProjectId,
            x.OccurredAtUtc
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.ActorId,
            x.OccurredAtUtc
        });

        builder.HasIndex(x => x.CorrelationId);

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

        builder.HasOne(x => x.Package)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.PackageId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Actor)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);
    }
}