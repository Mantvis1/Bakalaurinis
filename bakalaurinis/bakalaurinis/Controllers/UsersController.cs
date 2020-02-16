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
            var user = _userService.Authenticate(authenticateDto.Username, authenticateDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // [Authorize]
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
