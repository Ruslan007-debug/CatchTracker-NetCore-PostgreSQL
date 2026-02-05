using CatchTrackerApi.DTOs.AuthDTOs;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatchTrackerApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var response = await _authService.RegisterAsync(registerDTO);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration.", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDTO);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", details = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var response = await _authService.RefreshTokenAsync(refreshTokenDTO.RefreshToken);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while refreshing the token.", details = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            if (string.IsNullOrEmpty(refreshTokenDTO.RefreshToken))
            {
                return BadRequest(new { message = "Refresh token is required for logout." });
            }
            try
            {
                var result = await _authService.LogoutAsync(refreshTokenDTO.RefreshToken);
                if (!result)
                {
                    return BadRequest(new { message = "Logout failed. Invalid refresh token." });
                }
                return Ok(new { message = "Successfully logged out." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during logout.", details = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID not found in token" });
            }
            try
            {
                var user = await _authService.GetCurrentUserAsync(int.Parse(userIdClaim));
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the current user.", details = ex.Message });
            }
        }

        [HttpPost("promote/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PromoteToAdmin([FromRoute] int id)
        {
            try
            {
                var userToAdmin = await _authService.PromoteToAdminAsync(id);
                return Ok(new { message = "User promoted to Admin successfully", userToAdmin });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while promoting the user to Admin.", details = ex.Message });
            }
        }
    }
}
