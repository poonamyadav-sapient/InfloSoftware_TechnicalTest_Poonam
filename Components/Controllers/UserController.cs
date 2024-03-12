using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExtentApplication_UserManagement.Components.Models;
using ExtentApplication_UserManagement.Components.Models.Data;

namespace ExtentApplication_UserManagement.Components.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly YourDbContext _context;
        private readonly UserService _userService;

        public UsersController(YourDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var registrationResult = await _userService.RegisterUserAsync(model);
            if (registrationResult)
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var loginResult = await _userService.LoginAsync(model);
            if (loginResult)
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var result = await _userService.AddUser(user);
            if (result)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUser(user);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.DeleteUser(id);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
