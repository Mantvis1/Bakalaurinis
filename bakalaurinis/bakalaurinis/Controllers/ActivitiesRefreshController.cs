using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesRefreshController : ControllerBase
    {
        private readonly IScheduleGenerationService _scheduleGenerationService;
        public ActivitiesRefreshController(IScheduleGenerationService scheduleGenerationService)
        {
            _scheduleGenerationService = scheduleGenerationService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            await _scheduleGenerationService.Generate(id);

            return NoContent();
        }
    }
}