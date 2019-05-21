using BookStore.Common.Extensions;
using BookStore.Controllers.RequestModels;
using BookStore.Services.Auth;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BookStore.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly RoleManager<IdentityRole> roleManger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly TokenFactory tokenFactory;
        private const string userRole = "User";

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManger)
        {
            this.userManger = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManger = roleManger ?? throw new ArgumentNullException(nameof(roleManger));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            tokenFactory = new TokenFactory();
        }

        /// <summary>
        /// Add new user in DB
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>auth token</returns>
        public async Task<string> AddUserAsync(IdentityUser user, string password)
        {
            var result = await userManger.CreateAsync(user, password);
            result.CheckResult();

            //TODO add to seeds
            if (!await roleManger.RoleExistsAsync(userRole))
            {
                await roleManger.CreateAsync(new IdentityRole(userRole));
            }

            var addRole = await userManger.AddToRoleAsync(user, userRole);
            addRole.CheckResult();

            return tokenFactory.GenerateToken(user, new List<string> { userRole });
        }

        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns>auth token</returns>
        public async Task<string> Authenticate(LoginUserRequest request)
        {
            var user = await userManger.FindByEmailAsync(request.Email);
            var sugnInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!sugnInResult.Succeeded)
            {
                throw new ValidationException("The data entered is not correct");
            }
            var roles = await userManger.GetRolesAsync(user);
            return tokenFactory.GenerateToken(user, roles);
        }        
    }
}
