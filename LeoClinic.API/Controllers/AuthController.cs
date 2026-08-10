using LeoClinic.Application.DTOs;
using LeoClinic.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LeoClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Registration data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.Register(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during registration.", Detail = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Login data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var authResponse = await _authService.Login(request);
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during login.", Detail = ex.Message });
            }
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Verification data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.VerifyEmail(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during email verification.", Detail = ex.Message });
            }
        }

        [HttpPost("resend-verification-code")]
        public async Task<IActionResult> ResendVerificationCode([FromBody] ResendCodeRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.ResendVerificationCode(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred while resending verification code.", Detail = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized(new { Message = "Refresh token is required." });
            }

            try
            {
                var authResponse = await _authService.RefreshToken(refreshToken);
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during token refresh.", Detail = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            try
            {
                await _authService.Logout(refreshToken);
                return Ok(new { Message = "Logged out successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during logout.", Detail = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.ForgotPassword(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during forgot password request.", Detail = ex.Message });
            }
        }

        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.VerifyResetCode(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during reset code verification.", Detail = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data.", Errors = ModelState });
            }

            try
            {
                var message = await _authService.ResetPassword(request);
                return Ok(new { Message = message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred during password reset.", Detail = ex.Message });
            }
        }
    }
}
