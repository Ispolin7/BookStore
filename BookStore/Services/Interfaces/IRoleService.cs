using BookStore.Controllers.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> GetAllRolesAsync();
        //Task<IdentityRole> GetRoleAsync(string name);
        Task AddNewRoleAsync(RoleRequest newRole);
        Task UpdateRoleAsync(RoleRequest updatedRole);
        Task DeleteRoleAsync(Guid id);
    }
}
