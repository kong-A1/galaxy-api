using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

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

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("บทบาทของผู้ใช้"));
        builder.Property(x => x.Code)
            .HasComment("รหัสบทบาทสำหรับอ้างอิงในโค้ด ไม่ซ้ำ (นับเฉพาะแถวที่ยังไม่ถูกลบ)");
        builder.Property(x => x.Name)
            .HasComment("ชื่อบทบาทที่แสดงบนหน้าจอ");
        builder.Property(x => x.IsActive)
            .HasComment("เปิด/ปิดบทบาทชั่วคราว FALSE = ผู้ใช้ที่มีบทบาทนี้ไม่ได้รับสิทธิ์จากบทบาทนี้");
        builder.Property(x => x.IsSuperAdmin)
            .HasComment("TRUE = ผู้ดูแลสูงสุด ผ่านการตรวจสิทธิ์ทุกอย่าง");
        builder.Property(x => x.IsSystem)
            .HasComment("บทบาทที่ระบบสร้างให้ (seed) ห้ามแก้ไขหรือลบผ่านหน้าจอ");
    }
}
