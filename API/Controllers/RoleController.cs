using API.DTO.Request_Response;
using API.Entites;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        [HttpPost]
        public async Task<IActionResult> createRole([FromBody] CreateRoleRequest req)
        {
            var role = new Roles
            {
                RoleName = req.RoleName,
                Description = req.Description
            };
            role= await roleRepository.CreateRole(role);

            return Ok(role);
        }
    }
}
