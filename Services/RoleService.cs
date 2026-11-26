using galaxy_api.Data;
using galaxy_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using galaxy_api.Dtos.Common;
using galaxy_api.Dtos.Responses;
using galaxy_api.Dtos.Requests;
using galaxy_api.Entities;
using Npgsql;

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

    public async Task<CreateRolesResult> CreateRolesAsync(CreateRolesRequest request, CancellationToken ct)
    {
        var roles = request.Roles
            .Select(x => new Role
            {
                Code = x.Code,
                Name = x.Name.Trim()
            })
            .ToList();

        db.Roles.AddRange(roles);

        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException e) when (e.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation,
            ConstraintName: "ix_roles_code"
        })
        {
            var codes = roles.Select(x => x.Code).ToList();

            var duplicates = await db.Roles
                .Where(x => codes.Contains(x.Code))
                .Select(x => x.Code)
                .ToListAsync(ct);

            return new CreateRolesResult([], duplicates);
        }

        var created = roles
            .Select(x => new RoleResponse(
                x.Id, x.Code, x.Name, x.IsActive, x.IsSuperAdmin, x.IsSystem))
            .ToList();

        return new CreateRolesResult(created, []);
    }
}