using galaxy_api.Dtos.Responses;

namespace galaxy_api.Interfaces;

public interface IRoleService
{
    Task<List<RoleResponse>> GetRolesAsync(CancellationToken ct);
}