using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class EntityRevisionConfiguration
    : IEntityTypeConfiguration<EntityRevision>
{
    public void Configure(
        EntityTypeBuilder<EntityRevision> builder)
    {
        builder.ToTable("EntityRevisions", table =>
        {
            table.HasCheckConstraint(
                "CK_EntityRevisions_PositiveVersion",
                "\"Version\" > 0");

            table.HasCheckConstraint(
                "CK_EntityRevisions_ChangeReason",
                "length(btrim(\"ChangeReason\")) > 0");

            table.HasCheckConstraint(
                "CK_EntityRevisions_SnapshotObject",
                "jsonb_typeof(\"SnapshotJson\") = 'object'");
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EntityId)
            .IsRequired();

        builder.Property(x => x.EntityType)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Version)
            .IsRequired();

        builder.Property(x => x.SnapshotJson)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(x => x.ChangeReason)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.EntityType,
            x.EntityId,
            x.Version
        }).IsUnique();

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.ProjectId,
            x.RecordedAtUtc
        });

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

        builder.HasOne(x => x.ChangedByActor)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ChangedByActorId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.Id
            })
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.RecordedAtUtc)
            .IsRequired();
    }
}