using AutoMapper;
using BookStore.Controllers.Filters;
using BookStore.Controllers.RequestModels;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserRequest loginUser)
        {
            var token = await this.accountService.Authenticate(loginUser);
            return Ok(new { access_token = token});
        }

        [HttpPost]
        [Route("register")]
        [ValidateModelState]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequest newUser)
        {
            var token = await this.accountService.AddUserAsync(this.mapper.Map<IdentityUser>(newUser), newUser.Password);
            return Ok(new { access_token = token });
        }
    }
}