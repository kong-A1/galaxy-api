using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class MenuPermissionConfiguration : IEntityTypeConfiguration<MenuPermission>
{
    public void Configure(EntityTypeBuilder<MenuPermission> builder)
    {
        builder.ToTable("menu_permissions");
        builder.HasKey(x => new { x.MenuId, x.PermissionId });

        builder.HasOne<MasterMenu>().WithMany()
            .HasForeignKey(x => x.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Permission>().WithMany()
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.PermissionId);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("สิทธิ์ที่ใช้ควบคุมการเห็นเมนู"));
        builder.Property(x => x.MenuId)
            .HasComment("เมนูที่ถูกควบคุมสิทธิ์ (FK master_menus.id)");
        builder.Property(x => x.PermissionId)
            .HasComment("สิทธิ์ที่ต้องมีเพื่อเห็นเมนูนี้ (FK permissions.id)");
    }
}
