using galaxy_api.Models;
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
    }
}