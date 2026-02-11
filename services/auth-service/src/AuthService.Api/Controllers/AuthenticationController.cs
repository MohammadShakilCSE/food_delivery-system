using AuthService.Api.Contracts.Authentication;
using AuthService.Application.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("auth/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await _authService.RegisterAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            return Ok(authResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await _authService.LoginAsync(
                request.Email,
                request.Password);

            return Ok(authResult);
        }

    }
}
