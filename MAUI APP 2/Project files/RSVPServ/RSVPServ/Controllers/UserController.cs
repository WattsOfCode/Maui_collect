using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RSVPServ.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RSVPServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User>();

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            if (_users.Any(u => u.Username == newUser.Username))
            { return Conflict("Username already exists"); }
            if (newUser == null)
            { return BadRequest("User data is null"); 
            }

            _users.Add(newUser);
            return Ok(new { message = "User registered on RSVPServ!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var user = _users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user != null) return Ok(user);
            return Unauthorized();
        }

        public static bool UserExists(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
