using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.TokenHash).HasMaxLength(64);
        builder.Property(x => x.CreatedIp).HasMaxLength(45);
        builder.Property(x => x.UserAgent).HasMaxLength(255);

        builder.HasIndex(x => x.TokenHash).IsUnique(); // ค้น token และห้ามซ้ำ
        builder.HasIndex(x => x.UserId);               // หา token ของ user
        builder.HasIndex(x => x.FamilyId);             // ยกเลิกทั้งกลุ่ม
        builder.HasIndex(x => x.ExpiresAt);            // ล้างที่หมดอายุ

        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("refresh token สำหรับต่ออายุการล็อกอิน (เก็บเฉพาะ hash)"));
        builder.Property(x => x.Id)
            .HasComment("รหัสโทเคน (UUID)");
        builder.Property(x => x.UserId)
            .HasComment("ผู้ใช้เจ้าของโทเคน (FK users.id)");
        builder.Property(x => x.TokenHash)
            .HasComment("ค่า hash ของ refresh token เก็บเฉพาะ hash ห้ามเก็บโทเคนจริง");
        builder.Property(x => x.FamilyId)
            .HasComment("รหัสกลุ่มโทเคนที่หมุนเวียนต่อกันจากการล็อกอินครั้งเดียว ใช้ตรวจจับการใช้โทเคนซ้ำ");
        builder.Property(x => x.ExpiresAt)
            .HasComment("เวลาที่โทเคนหมดอายุ (UTC)");
        builder.Property(x => x.RevokedAt)
            .HasComment("เวลาที่โทเคนถูกเพิกถอน ว่าง = ยังไม่ถูกเพิกถอน");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่ออกโทเคน (UTC)");
        builder.Property(x => x.CreatedIp)
            .HasComment("IP ของผู้ที่ขอโทเคน");
        builder.Property(x => x.UserAgent)
            .HasComment("User-Agent ของอุปกรณ์/เบราว์เซอร์ที่ขอโทเคน");
    }
}
