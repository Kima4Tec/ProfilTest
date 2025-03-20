using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfilTest.Models;
using ProfilTest.Services;

namespace ProfilTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Users user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(new { message = "Brugernavn og kodeord kræves." });
            }

            var success = _authService.RegisterUser(user.UserName, user.Password);
            if (!success)
            {
                return BadRequest(new { message = "Brugernavn er allerede taget." });
            }

            return Ok(new { message = "Bruger registreret." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _authService.GetUserByUsername(request.UserName);
            if (user != null && _authService.VerifyPassword(request.Password, user.Password))
            {
                return Ok(new { message = "Login succesfuldt" });
            }
            return Unauthorized(new { message = "Ugyldigt login" });
        }
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _authService.GetUsers(); // Tilføj en metode til at hente alle brugere
            return Ok(users);
        }
    }



    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
