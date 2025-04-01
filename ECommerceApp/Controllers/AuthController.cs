using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IUserRepository userRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (model.Roles != null && model.Roles.Any())
                {
                    var IdentityResult = await userManager.AddToRolesAsync(user, model.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        await userRepository.CreateUserAsync(new UserDto { Email = model.Email });
                        return Ok("User "+ model.Email+" was registered! Please Login.");
                    }
                }
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null) { 
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, model.Password);
                if (checkPasswordResult) {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null) { 
                        var jwtToken = tokenRepository.GenerateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Invalid login attempt.");
        }
    }
}
