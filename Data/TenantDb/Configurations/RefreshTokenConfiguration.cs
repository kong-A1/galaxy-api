using galaxy_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.TenantDb.Configurations;

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
    }
}