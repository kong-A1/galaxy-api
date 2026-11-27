using galaxy_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using galaxy_api.Dtos.Requests;
using galaxy_api.Dtos.Responses;
namespace galaxy_api.Controllers;

[ApiController]
[Route("roles")]
public class RolesController(IRoleService roleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RoleResponse>>> GetRoles(CancellationToken ct)
    {
        var roles = await roleService.GetRolesAsync(ct);
        return Ok(roles);
    }

    [HttpPost]
    public async Task<ActionResult<List<RoleResponse>>> CreateRoles(CreateRolesRequest request, CancellationToken ct)
    {
        var result = await roleService.CreateRolesAsync(request, ct);

        if (result.Created.Count == 0)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Role code already exists",
                detail: $"Duplicate codes: {string.Join(", ", result.DuplicateCodes)}");
        }

        return Created("/roles", result.Created);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoleResponse>> GetRoleById(Guid id, CancellationToken ct)
    {
        var role = await roleService.GetRoleByIdAsync(id, ct);

        if (role is null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Role not found");
        }

        return Ok(role);
    }
}