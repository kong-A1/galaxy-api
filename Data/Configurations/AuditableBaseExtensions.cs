using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public static class AuditableBaseExtensions
{
    public static void ConfigureAuditable<T>(this EntityTypeBuilder<T> builder)
        where T : AuditableBase
    {
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        // คำอธิบายคอลัมน์ที่ใช้ร่วมกัน
        builder.Property(x => x.Id)
            .HasComment("รหัสของแถว (UUID)");
        builder.Property(x => x.CreatedBy)
            .HasComment("ผู้ใช้ที่สร้างแถวนี้ (ว่างได้สำหรับข้อมูลที่ระบบสร้าง)");
        builder.Property(x => x.UpdatedBy)
            .HasComment("ผู้ใช้ที่แก้ไขแถวนี้ล่าสุด");
        builder.Property(x => x.DeletedBy)
            .HasComment("ผู้ใช้ที่ลบแถวนี้ (soft delete)");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่สร้างแถว (เก็บเป็น UTC)");
        builder.Property(x => x.UpdatedAt)
            .HasComment("เวลาที่แก้ไขล่าสุด (UTC)");
        builder.Property(x => x.DeletedAt)
            .HasComment("เวลาที่ลบแบบ soft delete ว่าง = ยังใช้งานอยู่");

        builder.HasQueryFilter(x => x.DeletedAt == null);

        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}