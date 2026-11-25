using galaxy_api.Models;
using Microsoft.EntityFrameworkCore;

namespace galaxy_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Permission> Permissions => Set<Permission>();
}