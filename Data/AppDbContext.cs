using galaxy_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using galaxy_api.Data.Configurations;

namespace galaxy_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly,
            type => type.Namespace == typeof(UserConfiguration).Namespace);
    }

    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<PermissionRole> PermissionRoles => Set<PermissionRole>();
    public DbSet<MasterMenu> MasterMenus => Set<MasterMenu>();
    public DbSet<MenuPermission> MenuPermissions => Set<MenuPermission>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
}