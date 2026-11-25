using galaxy_api.Entities;
using galaxy_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace galaxy_api.Data.Interceptors;

public class AuditInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var now = DateTimeOffset.UtcNow;
        var userId = currentUser.UserId;

        foreach (var entry in context.ChangeTracker.Entries<ITimestamped>())
        {
            if (entry.State == EntityState.Added)
            {
                // เพิ่มใหม่
                entry.Property(x => x.CreatedAt).CurrentValue = now;
                entry.Property(x => x.UpdatedAt).CurrentValue = now;

                if (entry.Entity is AuditableBase created)
                {
                    created.CreatedBy ??= userId;
                    created.UpdatedBy ??= userId;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                // แก้ไข
                entry.Property(x => x.UpdatedAt).CurrentValue = now;

                if (entry.Entity is AuditableBase)
                {
                    entry.Property(nameof(AuditableBase.UpdatedBy)).CurrentValue = userId;
                }
            }
            else if (entry.State == EntityState.Deleted && entry.Entity is AuditableBase)
            {
                // ลบ → เปลี่ยนเป็น soft delete
                entry.State = EntityState.Unchanged;

                entry.Property(nameof(AuditableBase.DeletedAt)).CurrentValue = now;
                entry.Property(nameof(AuditableBase.DeletedBy)).CurrentValue = userId;
                entry.Property(x => x.UpdatedAt).CurrentValue = now;
                entry.Property(nameof(AuditableBase.UpdatedBy)).CurrentValue = userId;
            }
        }
    }
}