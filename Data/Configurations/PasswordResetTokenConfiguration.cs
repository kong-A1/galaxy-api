using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{
    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("password_reset_tokens");

        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.TokenHash).HasMaxLength(64);

        builder.HasIndex(x => x.TokenHash).IsUnique();
        builder.HasIndex(x => x.UserId);

        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("คำขอรีเซ็ตรหัสผ่าน (เก็บเฉพาะ hash ของโทเคน)"));
        builder.Property(x => x.Id)
            .HasComment("รหัสคำขอรีเซ็ตรหัสผ่าน (UUID)");
        builder.Property(x => x.UserId)
            .HasComment("ผู้ใช้ที่ขอรีเซ็ตรหัสผ่าน (FK users.id)");
        builder.Property(x => x.TokenHash)
            .HasComment("ค่า hash ของโทเคนที่ส่งทางอีเมล ห้ามเก็บโทเคนจริง");
        builder.Property(x => x.ExpiresAt)
            .HasComment("เวลาที่ลิงก์รีเซ็ตหมดอายุ (UTC)");
        builder.Property(x => x.UsedAt)
            .HasComment("เวลาที่ใช้โทเคนนี้แล้ว ว่าง = ยังไม่ถูกใช้");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่สร้างคำขอ (UTC)");
    }
}
