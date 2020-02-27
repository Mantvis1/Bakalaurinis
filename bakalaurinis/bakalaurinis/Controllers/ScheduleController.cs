using bakalaurinis.Dtos.Activity;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }


        [HttpGet("{userId}")]
        [Produces(typeof(ActivityDto[]))]
        public async Task<IActionResult> Get(int userId)
        {
            var activities = await _scheduleService.GetAllByUserIdFilterByDate(userId);

            if (activities == null)
                return NotFound();

            return Ok(activities);
        }

    }
}
