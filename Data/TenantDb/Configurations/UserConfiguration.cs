using galaxy_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.TenantDb.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", t =>
        {
            t.HasCheckConstraint("ck_users_failed_login_count",
                "failed_login_count >= 0");
            t.HasCheckConstraint("ck_users_deleted_by",
                "deleted_by IS NULL OR deleted_at IS NOT NULL");
        });
        builder.ConfigureAuditable();

        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(255);

        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.Property(x => x.FailedLoginCount).HasDefaultValue(0);
        builder.Property(x => x.MustChangePassword).HasDefaultValue(false);
    }
}