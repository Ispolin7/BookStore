using BookStore.Controllers.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> AddUserAsync(IdentityUser user, string password);
        Task<string> Authenticate(LoginUserRequest request);
    }
}
