using AuthService.Dto;
using AuthService.Interface;
using AuthService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;
        public AuthController(IAuthService authService,JwtService service)
        {
            _authService = authService;
            _jwtService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.Register(register);
            if (user == null) { return BadRequest("User with this email already exists."); }
            


            var token = _jwtService.GenerateToken(user.Email, user.Role);
            return Ok(new { Token = token, Message = "User registered successfully" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.Login(login);
            if (user == null)
                return Unauthorized("Invalid credentials.");
            var token=_jwtService.GenerateToken(user.Email,user.Role);

            return Ok(new { Token = token });

        }
    }
}
