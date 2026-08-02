using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Services;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        { 
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);
            if (!result)
                return BadRequest("Email already exists");

            return Ok("User registered successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto) 
        {
            var token = await _authService.Login(dto);
            if (token == null ) return Unauthorized("Invalid email or password");
            return Ok( new
            {
                Token = token,
            });
        }
    }
}
