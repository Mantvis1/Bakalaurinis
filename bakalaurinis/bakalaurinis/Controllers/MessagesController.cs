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

        [HttpGet("{userId}")]
        [Produces(typeof(MessageDto))]
        public async Task<IActionResult> Get(int userId)
        {
            var message = await _messageService.GetById(userId);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpDelete("{userId}/{messageId}")]
        public async Task<IActionResult> Delete(int userId, int messageId)
        {
            await _messageService.Delete(userId, messageId);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _messageService.Delete(userId);

            return NoContent();
        }


    }
}
