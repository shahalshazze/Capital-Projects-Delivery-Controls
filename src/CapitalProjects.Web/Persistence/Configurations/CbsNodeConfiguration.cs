using CapitalProjects.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalProjects.Web.Persistence.Configurations;

public sealed class CbsNodeConfiguration
    : IEntityTypeConfiguration<CbsNode>
{
    public void Configure(EntityTypeBuilder<CbsNode> builder)
    {
        builder.ToTable("CbsNodes", table =>
        {
            table.HasCheckConstraint(
                "CK_CbsNodes_NoSelfParent",
                "\"ParentId\" IS NULL OR \"ParentId\" <> \"Id\"");
        });

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

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

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

        builder.HasOne(x => x.Parent)
            .WithMany()
            .HasForeignKey(x => new
            {
                x.TenantId,
                x.ProjectId,
                x.ParentId
            })
            .HasPrincipalKey(x => new
            {
                x.TenantId,
                x.ProjectId,
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