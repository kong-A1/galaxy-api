using galaxy_api.Dtos.Common;
using galaxy_api.Dtos.Requests;
using galaxy_api.Dtos.Responses;

namespace galaxy_api.Interfaces;

public interface IRoleService
{
    Task<List<RoleResponse>> GetRolesAsync(CancellationToken ct);
    Task<CreateRolesResult> CreateRolesAsync(CreateRolesRequest request, CancellationToken ct);
}