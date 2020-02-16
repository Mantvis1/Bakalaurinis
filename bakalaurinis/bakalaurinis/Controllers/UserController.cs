using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using bakalaurinis.Services.Interfaces;
using bakalaurinis.Dtos.User;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

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
    }
}