using galaxy_api.Dtos.Responses;
using galaxy_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
}