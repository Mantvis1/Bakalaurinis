using bakalaurinis.Dtos.Message;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        protected readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("all/{userId}")]
        [Produces(typeof(MessageDto[]))]
        public async Task<IActionResult> GetAll(int userId)
        {
            var messages = await _messageService.GetAll(userId);

            if (messages == null)
            {
                return NotFound();
            }

            return Ok(messages);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> Delete(int messageId)
        {
            await _messageService.DeleteById(messageId);

            return NoContent();
        }

        [HttpDelete("all/{userId}")]
        public async Task<IActionResult> DeleteAll(int userId)
        {
            await _messageService.Delete(userId);

            return NoContent();
        }


    }
}
