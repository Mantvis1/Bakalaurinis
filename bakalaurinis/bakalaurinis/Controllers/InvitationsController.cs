using bakalaurinis.Dtos.Invitation;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var invitationId = await _invitationService.Create(newInvitationDto);

                return Ok(invitationId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{invitationId}")]
        public async Task<IActionResult> Update(int invitationId, UpdateInvitationDto updateInvitationDto)
        {
            var isUpdated = await _invitationService.Update(invitationId, updateInvitationDto);

            return Ok(isUpdated);
        }

        [HttpGet("{receiverId}")]
        [Produces(typeof(InvitationDto[]))]
        public async Task<IActionResult> GetByReciever(int receiverId)
        {
            var invitations = await _invitationService.GetAllByReceiverId(receiverId);

            if (invitations == null)
            {
                return NotFound();
            }

            return Ok(invitations);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _invitationService.Delete(id);

            return Ok(isDeleted);
        }
    }
}
