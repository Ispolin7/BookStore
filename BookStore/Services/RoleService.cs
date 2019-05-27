using BookStore.Common.Extensions;
using BookStore.Controllers.RequestModels;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManger;

        public RoleService(RoleManager<IdentityRole> manager)
        {
            this.roleManger = manager ?? throw new ArgumentNullException(nameof(roleManger));
        }

        /// <summary>
        /// Add new role to DB
        /// </summary>
        /// <param name="newRole">role name</param>
        /// <returns></returns>
        public async Task AddNewRoleAsync(RoleRequest newRole)
        {
            var result = await this.roleManger.CreateAsync(new IdentityRole(newRole.Name));
            result.CheckResult();
        }

        /// <summary>
        /// Get role list
        /// </summary>
        /// <returns>role list</returns>
        public IEnumerable<IdentityRole> GetAllRolesAsync()
        {
            return this.roleManger.Roles.ToList();
        }

        /// <summary>
        /// Update role name
        /// </summary>
        /// <param name="updatedRole">new role information</param>
        /// <returns></returns>
        public async Task UpdateRoleAsync(RoleRequest updatedRole)
        {
            var role = await this.roleManger.FindByIdAsync(updatedRole.Id.ToString());
            role.Name = updatedRole.Name;
            var result = await roleManger.UpdateAsync(role);
            result.CheckResult();
        }

        /// <summary>
        /// REmove role from DB
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await this.roleManger.FindByIdAsync(id.ToString());
            var result = await this.roleManger.DeleteAsync(role);
            result.CheckResult();
        }
    }
}
