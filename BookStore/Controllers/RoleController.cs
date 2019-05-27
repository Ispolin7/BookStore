using AutoMapper;
using BookStore.Controllers.Filters;
using BookStore.Controllers.RequestModels;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/roles")]
    [Produces("application/json")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService service, IMapper mapper)
        {
            this.roleService = service ?? throw new ArgumentNullException(nameof(roleService));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAll()
        {
            var roles = this.roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModelState]
        public async Task<ActionResult> AddAsync([FromBody] RoleRequest newRole)
        {
            await this.roleService.AddNewRoleAsync(newRole);
            //return Created(newRole.Name, null);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        [ValidateModelState]
        public async Task<ActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] RoleRequest role)
        {
            role.Id = id;
            await this.roleService.UpdateRoleAsync(role);
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await this.roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}