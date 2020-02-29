using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleGenerationController : ControllerBase
    {
        private readonly IScheduleGenerationService _scheduleGenerationService;
        public ScheduleGenerationController(IScheduleGenerationService scheduleGenerationService)
        {
            _scheduleGenerationService = scheduleGenerationService;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
           var isGenerated = await _scheduleGenerationService.Generate(userId);

           return Ok(isGenerated);
        }

    }
}
