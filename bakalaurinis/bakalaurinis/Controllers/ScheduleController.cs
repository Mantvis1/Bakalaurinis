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
        private readonly IScheduleGenerationService _scheduleGenerationService;
        public ScheduleController(IScheduleService scheduleService ,IScheduleGenerationService scheduleGenerationService)
        {
            _scheduleService = scheduleService;
            _scheduleGenerationService = scheduleGenerationService;
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

        [HttpPut("{userId}")]
        public async Task<IActionResult> Post(UpdateActivitiesDto updateActivitiesDto)
        {
            await _scheduleGenerationService.CalculateActivitiesTime(updateActivitiesDto);

            return Ok();
        }
    }
}
