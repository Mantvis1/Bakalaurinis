using bakalaurinis.Dtos.User;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthenticationService _authenticationService;
        //public AuthController(IAuthenticationService authenticationService)
        //{
        //    _authenticationService = authenticationService;
        //}

        //[Route("login")]
        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(AuthenticateDto authenticateDto)
        //{
        //    if (authenticateDto.Email != null && authenticateDto.Password != null)
        //    {
        //        var result = await _authenticationService.Authenticate(authenticateDto.Email, authenticateDto.Password);

        //        if (result != null)
        //        {
        //            return Ok(new
        //            {
        //                result.ClientId,
        //                result.Client.Token
        //            });
        //        }

        //        return Unauthorized();
        //    }
        //    return BadRequest();
        //}

        //[HttpGet]
        //[Route("roles")]
        //public IActionResult GetRoles()
        //{
        //    //  var roles = await _authenticationService.GetAllRoles();
        //    //  return Ok(roles);
        //    return NotFound();
        //}

        //[HttpPost]
        //[Route("register")]
        //public IActionResult Register()
        //{
        //    return NotFound();
        //}
    }
}
