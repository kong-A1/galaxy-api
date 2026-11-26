using galaxy_api.Data;
using galaxy_api.Dtos.Responses;
using galaxy_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace galaxy_api.Services;

public class RoleService(AppDbContext db) : IRoleService
{
    public async Task<List<RoleResponse>> GetRolesAsync(CancellationToken ct)
    {
        return await db.Roles
            .OrderBy(x => x.Code)
            .Select(x => new RoleResponse(
                x.Id, x.Code, x.Name, x.IsActive, x.IsSuperAdmin, x.IsSystem))
            .ToListAsync(ct);
    }
}