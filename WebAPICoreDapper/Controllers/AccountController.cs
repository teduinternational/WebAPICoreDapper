using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPICoreDapper.Extensions;
using WebAPICoreDapper.Filters;
using WebAPICoreDapper.Models;
using WebAPICoreDapper.ViewModels;

namespace WebAPICoreDapper.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                if (!result.Succeeded)
                    return BadRequest("Mật khẩu không đúng");
                var roles = await _userManager.GetRolesAsync(user);
                //var permissions = await _permissionService.GetPermissionStringByUserId(user.Id.ToString());
                var claims = new[]
                {
                    new Claim("Email", user.Email),
                    //new Claim(SystemConstants.UserClaim.Id, user.Id.ToString()),
                    //new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    //new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim(SystemConstants.UserClaim.FullName, user.FullName),
                    //new Claim(SystemConstants.UserClaim.Avatar, string.IsNullOrEmpty(user.Avatar) ? string.Empty : user.Avatar),
                    //new Claim(SystemConstants.UserClaim.Roles, string.Join(";", roles)),
                    //new Claim(SystemConstants.UserClaim.Permissions, JsonConvert.SerializeObject(permissions)),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                    // claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return NotFound($"Không tìm thấy tài khoản {model.UserName}");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ValidateModel]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new AppUser { FullName = model.FullName, UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                //await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));

                //await _signInManager.SignInAsync(user, false);

                return  Ok(model);
            }

            return BadRequest();
        }

    }
}