using Backend.Customization.SecurityInfra;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        SecurityManagement security;
        SecurityResponse response;

        public SecurityController(SecurityManagement security)
        {
            this.security = security;
            response = new SecurityResponse();
        }

        [HttpPost("register")]
      
        public async Task<IActionResult> RegisterUserAsync(AppUser user)
        {
            try
            {
                var result = await security.RegisterUserAsync(user);
                if (result)
                {
                    response.Message = $"User {user.Email} is created successfully";
                    return Ok(response);
                }
                else
                {
                    return BadRequest("User registration failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }




        [HttpPost("auth")]
        public async Task<IActionResult> AuthenticateUserAsync(LoginUser user)
        {
            try
            {
                response = await security.AuthenticateUserAsync(user);
                if (response.IsLoggedIn)
                {
                    response.Message = $"User {user.Email} is authenticated successfully";
                    return Ok(response);
                }
                else
                {
                    return Unauthorized("Authentication failed. Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

     

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var isLogOut = await security.LogoutAsync();
                if (isLogOut)
                {
                    return Ok("Logged out Successfully");
                }
                else
                {
                    return BadRequest("Logout failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
