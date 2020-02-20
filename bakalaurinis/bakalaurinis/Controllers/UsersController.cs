using bakalaurinis.Dtos.User;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [Produces(typeof(AfterAutentificationDto))]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateDto authenticateDto)
        {
            var user = await _userService.Authenticate(authenticateDto);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Register([FromBody]RegistrationDto registrationDto)
        {
            await _userService.Register(registrationDto);

            return Ok(true);
        }

        [HttpGet("self/{id}")]
        [Produces(typeof(UserNameDto))]
        public async Task<IActionResult> GetUsername(int id)
        {
            var username = _userService.GetNameById(id);

            if (username == null)
                return NotFound();

            return Ok(username);
        }
    }
}
