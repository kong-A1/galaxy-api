using galaxy_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.TenantDb.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.ConfigureAuditable();

        builder.Property(x => x.Code).HasMaxLength(20);
        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasFilter("deleted_at IS NULL");

        builder.Property(x => x.Name).HasMaxLength(50);

        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.IsSuperAdmin).HasDefaultValue(false);
        builder.Property(x => x.IsSystem).HasDefaultValue(false);
    }
}