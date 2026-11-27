using galaxy_api.Dtos.Responses;

namespace galaxy_api.Dtos.Common;

public record CreateRolesResult(
    List<RoleResponse> Created,
    List<string> DuplicateCodes);