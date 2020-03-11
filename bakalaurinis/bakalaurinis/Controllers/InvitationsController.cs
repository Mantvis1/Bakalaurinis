using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [HttpPost]
        [Produces(typeof(int))]
        public async Task<IActionResult> Create([FromBody]NewInvitationDto newInvitationDto)
        {
            int invitationId = await _invitationService.Create(newInvitationDto);

            return Ok(invitationId);
        }

        [HttpPut("{invitationId}")]
        public async Task<IActionResult> Update(int invitationId, UpdateInvitationDto updateInvitationDto)
        {
            var isUpdated = await _invitationService.Update(invitationId, updateInvitationDto);

            return Ok(isUpdated);
        }

        [HttpGet("sender/{senderId}")]
        [Produces(typeof(InvitationDto[]))]
        public async Task<IActionResult> GetBySender(int senderId)
        {
            var invitations = await _invitationService.GetAllBySenderId(senderId);

            if(invitations == null)
            {
                return NotFound();
            }

            return Ok(invitations);
        }

        [HttpGet("receiver/{receiverId}")]
        [Produces(typeof(InvitationDto[]))]
        public async Task<IActionResult> GetByReciever(int receiverId)
        {
            var invitations = await _invitationService.GetAllByRecieverId(receiverId);

            if (invitations == null)
            {
                return NotFound();
            }

            return Ok(invitations);
        }
    }
}
