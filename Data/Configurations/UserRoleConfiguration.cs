using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(x => new { x.UserId, x.RoleId });
        builder.HasIndex(x => x.RoleId);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Role>().WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>().WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("บทบาทที่กำหนดให้ผู้ใช้"));
        builder.Property(x => x.UserId)
            .HasComment("ผู้ใช้ที่ได้รับบทบาท (FK users.id)");
        builder.Property(x => x.RoleId)
            .HasComment("บทบาทที่ถูกกำหนดให้ผู้ใช้ (FK roles.id)");
        builder.Property(x => x.CreatedBy)
            .HasComment("ผู้ใช้ที่เป็นคนกำหนดบทบาทนี้ให้");
        builder.Property(x => x.CreatedAt)
            .HasComment("เวลาที่กำหนดบทบาท (UTC)");
    }
}
