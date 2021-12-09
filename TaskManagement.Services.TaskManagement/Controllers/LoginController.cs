using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagement.Services.TaskManagement.Domain.Entities;

namespace TaskManagement.Services.TaskManagement.Controllers
{
    public class LoginController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<Guid>> Register(string userName, string password)
        {
            var user = new ApplicationUser { UserName = userName, Id = Guid.NewGuid() };
            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return StatusCode(500);
            }
            var addPasswordResult = await _userManager.AddPasswordAsync(user, password);
            if (!addPasswordResult.Succeeded)
            {
                return StatusCode(500);
            }

            //var claims = new List<Claim>
            //{
            //    new Claim("user", userName),
            //    new Claim("role", "Member")
            //};

            //await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

            return Ok(user.Id);
        }



        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return NotFound();
            }


            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (signInResult.Succeeded)
            {

                return Ok();
            }

            return StatusCode(500);
            

            //var claims = new List<Claim>
            //{
            //    new Claim("user", userName)
            //};

            //await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")));

        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
