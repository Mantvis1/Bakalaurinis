using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login()
        {
            //if (request.Email != null && request.Password != null)
            //{
            //    var result = await _authenticationService.Authenticate(request.Email, request.Password);

            //    if (result != null)
            //    {
            //        return Ok(new
            //        {
            //            result.EmployeeId,
            //            result.Employee.Token
            //        });
            //    }

            //    return Unauthorized();
            //}
            //return BadRequest();
            return NotFound();
        }

        [HttpGet]
        [Route("roles")]
        public IActionResult GetRoles()
        {
            //  var roles = await _authenticationService.GetAllRoles();
            //  return Ok(roles);
            return NotFound();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register()
        {
            return NotFound();
        }
    }
}
