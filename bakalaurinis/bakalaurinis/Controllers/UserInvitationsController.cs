using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bakalaurinis.Dtos.UserInvitations;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInvitationsController : ControllerBase
    {
        private readonly IUserInvitationService _userInvitationService;
        public UserInvitationsController(IUserInvitationService userInvitationService)
        {
            _userInvitationService = userInvitationService;
        }


        [HttpGet("{id}")]
        [Produces(typeof(UserInvitationsDto[]))]
        public async Task<IActionResult> Get(int id)
        {
            var userInvitations = await _userInvitationService.GetAllByActivityId(id);

            if (userInvitations == null)
            {
                return NotFound();
            }

            return Ok(userInvitations);
        }
    }
}
