using bakalaurinis.Dtos.Activity;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesService _activitiesService;
        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        [HttpPost]
        [Produces(typeof(int))]
        public async Task<IActionResult> Create([FromBody] NewActivityDto newActivityDto)
        {
            var newActivityId = await _activitiesService.Create(newActivityDto);

            return Ok(newActivityId);
        }

        [HttpGet]
        [Produces(typeof(ActivityDto[]))]
        public async Task<IActionResult> Get()
        {
            var activities = await _activitiesService.GetAll();

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpGet("{id}")]
        [Produces(typeof(ActivityDto))]
        public async Task<IActionResult> Get(int id)
        {
            var activity = await _activitiesService.GetById(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpGet("user/{userId}")]
        [Produces(typeof(ActivityDto[]))]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var activities = await _activitiesService.GetByUserId(userId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _activitiesService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Produces(typeof(int))]
        public async Task<IActionResult> Update(int id,[FromBody] NewActivityDto newActivityDto)
        {
            var ok = await _activitiesService.Update(id, newActivityDto);

            return Ok(ok);
        }
    }
}
