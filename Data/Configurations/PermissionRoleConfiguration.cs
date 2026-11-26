using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRole>
{
    public void Configure(EntityTypeBuilder<PermissionRole> builder)
    {
        builder.ToTable("permission_role");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });
        builder.HasIndex(x => x.PermissionId);

        builder.HasOne<Role>().WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Permission>().WithMany()
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("สิทธิ์ที่มอบให้บทบาท"));
        builder.Property(x => x.RoleId)
            .HasComment("บทบาทที่ได้รับสิทธิ์ (FK roles.id)");
        builder.Property(x => x.PermissionId)
            .HasComment("สิทธิ์ที่ถูกมอบให้บทบาท (FK permissions.id)");
    }
}
