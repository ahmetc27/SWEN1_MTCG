using Microsoft.AspNetCore.Mvc;

namespace MTCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> Users = new List<User>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {        
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Username and password cannot be empty.");
            }

            var existingUser = Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Conflict("User already exists.");
            }

            Users.Add(user);
            return Ok(new { Message = "User registered successfully", Username = user.Username });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var existingUser = Users.FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            if (existingUser == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = $"{existingUser.Username}-{Guid.NewGuid()}"; // GUI = Globally Unique Identifier

            return Ok(new { Message = "Login successful", Token = token });
        }
    }
}