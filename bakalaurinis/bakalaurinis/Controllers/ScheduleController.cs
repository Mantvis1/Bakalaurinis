using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;
using bakalaurinis.Dtos.Schedule;
using bakalaurinis.Services.Generation.Interfaces;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly INewJobGenerationService _newJobGenerationService;

        public ScheduleController(
            IScheduleService scheduleService,
            INewJobGenerationService newJobGenerationService
            )
        {
            _scheduleService = scheduleService;
            _newJobGenerationService = newJobGenerationService;
        }

        [HttpGet("{userId}/{date}")]
        [Produces(typeof(GetScheduleDto))]
        public async Task<IActionResult> Get(int userId, DateTime date)
        {
            var getScheduleDto = await _scheduleService.GetAllByUserIdFilterByDate(userId, date);

            if (getScheduleDto == null)
            {
                return NotFound();
            }

            return Ok(getScheduleDto);
        }

        [HttpPut("{userId}/{date}")]
        public async Task<IActionResult> Post(int userId, DateTime date, UpdateWorkDto updateActivitiesDto)
        {
            await _newJobGenerationService.CalculateActivitiesTime(userId, date, updateActivitiesDto);

            return Ok();
        }
    }
}
