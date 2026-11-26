using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.Configurations;

public class MasterMenuConfiguration : IEntityTypeConfiguration<MasterMenu>
{
    public void Configure(EntityTypeBuilder<MasterMenu> builder)
    {
        builder.ToTable("master_menus");
        builder.ConfigureAuditable();

        builder.Property(x => x.Code).HasMaxLength(20);
        builder.Property(x => x.NameTh).HasMaxLength(50);
        builder.Property(x => x.NameEn).HasMaxLength(50);
        builder.Property(x => x.Route).HasMaxLength(100);
        builder.Property(x => x.Icon).HasMaxLength(50);
        builder.Property(x => x.SortOrder).HasDefaultValue(0);
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasFilter("deleted_at IS NULL");

        builder.HasOne<MasterMenu>()
            .WithMany()
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("master_menus", t =>
        {
            t.HasCheckConstraint("ck_master_menus_parent_not_self", "parent_id <> id");
        });

        builder.HasIndex(x => x.ParentId);

        // คำอธิบายตารางและคอลัมน์
        builder.ToTable(t => t.HasComment("เมนูของระบบ"));
        builder.Property(x => x.ParentId)
            .HasComment("เมนูแม่ (FK master_menus.id) NULL = เมนูระดับบนสุด");
        builder.Property(x => x.Code)
            .HasComment("รหัสเมนูสำหรับอ้างอิงข้ามระบบ ไม่ซ้ำ ไม่เป็นค่าว่าง");
        builder.Property(x => x.NameTh)
            .HasComment("ชื่อเมนูภาษาไทย");
        builder.Property(x => x.NameEn)
            .HasComment("ชื่อเมนูภาษาอังกฤษ");
        builder.Property(x => x.Route)
            .HasComment("path ของหน้าจอที่เมนูนี้เปิด ว่างได้เมื่อเป็นเมนูกลุ่มที่ไม่มีหน้าของตัวเอง");
        builder.Property(x => x.Icon)
            .HasComment("ชื่อไอคอนที่แสดงข้างเมนู");
        builder.Property(x => x.SortOrder)
            .HasComment("ลำดับการแสดงภายใต้เมนูแม่เดียวกัน (น้อยก่อน)");
        builder.Property(x => x.IsActive)
            .HasComment("ซ่อน/แสดงเมนูโดยไม่ต้องลบ");
    }
}
