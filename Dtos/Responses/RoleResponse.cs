namespace galaxy_api.Dtos.Responses;

public record RoleResponse(
    Guid Id,
    string Code,
    string Name,
    bool IsActive,
    bool IsSuperAdmin,
    bool IsSystem
);