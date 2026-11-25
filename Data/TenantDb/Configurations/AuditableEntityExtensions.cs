using galaxy_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace galaxy_api.Data.TenantDb.Configurations;

public static class AuditableEntityExtensions
{
    public static void ConfigureAuditable<T>(this EntityTypeBuilder<T> builder)
        where T : AuditableEntity
    {
        builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

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