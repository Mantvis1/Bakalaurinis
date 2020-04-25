using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IScheduleGenerationService _scheduleGenerationService;
        public ScheduleController(IScheduleService scheduleService, IScheduleGenerationService scheduleGenerationService)
        {
            _scheduleService = scheduleService;
            _scheduleGenerationService = scheduleGenerationService;
        }


        [HttpGet("{userId}/{date}")]
        [Produces(typeof(WorkDto[]))]
        public async Task<IActionResult> Get(int userId, DateTime date)
        {
            var activities = await _scheduleService.GetAllByUserIdFilterByDate(userId, date);

            if (activities == null)
                return NotFound();

            return Ok(activities);
        }

        [HttpPut("{userId}/{date}")]
        public async Task<IActionResult> Post(int userId, DateTime date, UpdateWorkDto updateActivitiesDto)
        {
            await _scheduleGenerationService.CalculateActivitiesTime(userId, date, updateActivitiesDto);

            return Ok();
        }

        [HttpGet("{userId}")]
        [Produces(typeof(int))]
        public async Task<IActionResult> GetBusyness(int userId)
        {
            var busyness = await _scheduleService.GetBusyness(userId, DateTime.Now);

            return Ok(busyness);
        }
    }
}
