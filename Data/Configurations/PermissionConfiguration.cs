using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Code).HasMaxLength(100);
        builder.HasIndex(x => x.Code).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Module).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(255);

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("รายการสิทธิ์ในระบบ"));
        builder.Property(x => x.Id)
            .HasComment("รหัสสิทธิ์ (UUID)");
        builder.Property(x => x.Code)
            .HasComment("รหัสสิทธิ์รูปแบบ resource:action เช่น member:read ใช้ในโค้ดตรวจสิทธิ์ ไม่ซ้ำ");
        builder.Property(x => x.Name)
            .HasComment("ชื่อสิทธิ์ที่แสดงบนหน้าจอจัดการ role");
        builder.Property(x => x.Module)
            .HasComment("กลุ่มของสิทธิ์ ใช้จัดหมวดบนหน้าจอจัดการ role");
        builder.Property(x => x.Description)
            .HasComment("คำอธิบายว่าสิทธิ์นี้ทำอะไรได้บ้าง");
        builder.Property(x => x.IsActive)
            .HasComment("ปิดสิทธิ์ชั่วคราวโดยไม่ต้องลบ FALSE = ไม่มีผลกับทุก role");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่สิทธิ์นี้ถูกซิงก์เข้าฐานข้อมูลครั้งแรก (UTC)");
        builder.Property(x => x.UpdatedAt)
            .HasComment("เวลาที่แก้ไขล่าสุด (UTC)");
    }
}
