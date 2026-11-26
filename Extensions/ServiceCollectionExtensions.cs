using galaxy_api.Data;
using galaxy_api.Data.Interceptors;
using galaxy_api.Interfaces;
using galaxy_api.Services;
using Microsoft.EntityFrameworkCore;

namespace galaxy_api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IRoleService, RoleService>();

        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditInterceptor>();

        services.AddDbContext<AppDbContext>((sp, o) =>
            o.UseNpgsql(configuration.GetConnectionString("Default"))
             .UseSnakeCaseNamingConvention()
             .AddInterceptors(sp.GetRequiredService<AuditInterceptor>()));

        return services;
    }
}
