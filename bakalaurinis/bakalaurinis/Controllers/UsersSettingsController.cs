using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Services.Interfaces;
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

        [HttpPut("{userId}")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Update([FromBody]UserSettingsDto userSettingsDto)
        {
            var isUpdated = await _userSettingsService.Update(userSettingsDto);

            return Ok(isUpdated);
        }

        [HttpPut("itemsPerPage/{userId}")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Update([FromBody]UpdateUserItemsPerPageSettings userSettingsDto)
        {
            var isUpdated = await _userSettingsService.Update(userSettingsDto);

            return Ok(isUpdated);
        }


        [HttpGet("{userId}")]
        [Produces(typeof(UserSettingsDto))]
        public async Task<IActionResult> Get(int userId)
        {
            var settings = await _userSettingsService.GetByUserId(userId);

            return Ok(settings);
        }

        [HttpGet("itemsPerPage/{userId}")]
        [Produces(typeof(GetUserItemsPerPageSetting))]
        public async Task<IActionResult> GetItemsPerPage(int userId)
        {
            var settings = await _userSettingsService.GetUserItemsPerPageSetting(userId);

            return Ok(settings);
        }
    }
}
