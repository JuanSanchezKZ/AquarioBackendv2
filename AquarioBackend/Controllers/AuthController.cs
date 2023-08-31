using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AquarioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController (UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var users = await userManager.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] string id)
        { 
           var user = await userManager.FindByIdAsync(id);

           return Ok(user);

        }

        // POST: /api/auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any()) 
                {
                    await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered!");
                    }
                }
                

            }

            return BadRequest("Something went wrong");
        }



        // POST : /api/Auth/Login/

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (user != null)
            {
               var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (checkPasswordResult)
                {

                    var roles = await userManager.GetRolesAsync(user);
                    // Create Token

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user , roles.ToList());

                        var response = new LoginResponseDTO {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);

                    }
                }

            
            }

            return BadRequest("Username or password incorrect");

        }
    }
}
