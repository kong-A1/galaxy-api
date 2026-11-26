using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

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

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("ผู้ใช้ที่ล็อกอินเข้าระบบ"));
        builder.Property(x => x.Name)
            .HasComment("ชื่อ-นามสกุลของพนักงานที่แสดงในระบบ");
        builder.Property(x => x.Email)
            .HasComment("อีเมลที่ใช้ล็อกอิน ไม่ซ้ำ (นับเฉพาะผู้ใช้ที่ยังไม่ถูกลบ ไม่แยกตัวพิมพ์เล็ก/ใหญ่)");
        builder.Property(x => x.PasswordHash)
            .HasComment("รหัสผ่านที่ผ่านการ hash แล้ว (เช่น Argon2id/bcrypt) ห้ามเก็บรหัสผ่านจริง");
        builder.Property(x => x.IsActive)
            .HasComment("เปิด/ปิดบัญชีชั่วคราว FALSE = ล็อกอินไม่ได้ (ต่างจากการลบ ซึ่งใช้ deleted_at)");
        builder.Property(x => x.LastLoginAt)
            .HasComment("เวลาที่ล็อกอินสำเร็จล่าสุด (UTC)");
        builder.Property(x => x.FailedLoginCount)
            .HasComment("จำนวนครั้งที่ใส่รหัสผ่านผิดติดต่อกัน ใช้ล็อกบัญชีเมื่อเกินกำหนด");
        builder.Property(x => x.LockedUntil)
            .HasComment("เวลาที่บัญชีถูกล็อกจนถึง ว่าง = ไม่ถูกล็อก");
        builder.Property(x => x.PasswordChangedAt)
            .HasComment("เวลาที่เปลี่ยนรหัสผ่านล่าสุด (UTC)");
        builder.Property(x => x.MustChangePassword)
            .HasComment("บังคับเปลี่ยนรหัสผ่านเมื่อล็อกอินครั้งถัดไป");
    }
}
