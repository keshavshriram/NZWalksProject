using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult = await userManager.CreateAsync( identityUser , registerRequestDto.Password);

            if (identityResult.Succeeded)
            { //Add  roles to this user  
                if ( registerRequestDto.Roles != null )
                {
                    identityResult = await userManager.AddToRoleAsync(identityUser , registerRequestDto.Roles);
                    return Ok("User is registered successfully, please login.");
                }

            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login( [FromBody] LoginRequestDto loginRequestDto )
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user , loginRequestDto.Password);
                var roles = await userManager.GetRolesAsync(user);
                if (checkPasswordResult)
                {
                    // Create Token  

                    var jwtToken = tokenRepository.CreateJWTToken(user,roles.ToList());
                    var response = new LoginResponseDTO()
                    {
                        jwtToken = jwtToken
                    };

                    return Ok(response);
                }
            }
            return BadRequest("Entered username or password is incorrect . ");
        }
    }
}
