using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}
