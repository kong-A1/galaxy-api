using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs", t =>
        {
            t.HasCheckConstraint("ck_audit_logs_actor_type",
                "actor_type IN ('user','system','platform')");
        });

        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.ActorType).HasMaxLength(20);
        builder.Property(x => x.Action).HasMaxLength(50);
        builder.Property(x => x.EntityType).HasMaxLength(50);
        builder.Property(x => x.IpAddress).HasMaxLength(45);
        builder.Property(x => x.UserAgent).HasMaxLength(255);

        builder.Property(x => x.OldValues).HasColumnType("jsonb");
        builder.Property(x => x.NewValues).HasColumnType("jsonb");

        builder.HasIndex(x => new { x.EntityType, x.EntityId });
        builder.HasIndex(x => new { x.ActorUserId, x.CreatedAt });
        builder.HasIndex(x => x.CreatedAt);

        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.ActorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("ประวัติการกระทำในระบบ"));
        builder.Property(x => x.Id)
            .HasComment("รหัสบันทึก audit (UUID)");
        builder.Property(x => x.ActorType)
            .HasComment("ประเภทผู้กระทำ: user = ผู้ใช้ในระบบ, system = ระบบ, platform = ค่าจากแบบเดิม (รอทบทวน)");
        builder.Property(x => x.ActorUserId)
            .HasComment("ผู้ใช้ที่ทำรายการ (FK users.id)");
        builder.Property(x => x.PlatformActorId)
            .HasComment("คอลัมน์จากแบบเดิมที่มีฐานกลาง ไม่มี FK (รอทบทวน)");
        builder.Property(x => x.Action)
            .HasComment("ชื่อการกระทำ");
        builder.Property(x => x.EntityType)
            .HasComment("ประเภทข้อมูลที่ถูกกระทำ เช่น role, user, menu");
        builder.Property(x => x.EntityId)
            .HasComment("รหัสของข้อมูลที่ถูกกระทำ");
        builder.Property(x => x.OldValues)
            .HasComment("ค่าก่อนเปลี่ยน");
        builder.Property(x => x.NewValues)
            .HasComment("ค่าหลังเปลี่ยน");
        builder.Property(x => x.IpAddress)
            .HasComment("IP ของผู้กระทำ");
        builder.Property(x => x.UserAgent)
            .HasComment("User-Agent ของผู้กระทำ");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่เกิดเหตุการณ์ (UTC)");
    }
}
