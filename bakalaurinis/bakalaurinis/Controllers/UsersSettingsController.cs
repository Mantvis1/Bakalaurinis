using bakalaurinis.Dtos.User;
using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _userSettingsService;

        public UsersSettingsController(IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
        }

        public async Task<IActionResult> Update([FromBody]UserSettingsDto suerSettingsDto)
        {

            return Ok(true);
        }


        [HttpGet("{userId}")]
        [Produces(typeof(UserSettingsDto))]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(true);
        }
    }
}
