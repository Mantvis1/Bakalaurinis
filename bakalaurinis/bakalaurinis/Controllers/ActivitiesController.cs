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
        private readonly IWorksService _activitiesService;
        public ActivitiesController(IWorksService activitiesService)
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

        [HttpGet("status/{workId}")]
        [Produces(typeof(WorkStatusConfirmationDto))]
        public async Task<IActionResult> GetStatus(int workId)
        {
            var activityStatus = await _activitiesService.GetWorkConfirmationStatusById(workId);

            if (activityStatus == null)
            {
                return NotFound();
            }

            return Ok(activityStatus);
        }

        [HttpPut("status/{workId}")]
        public async Task<IActionResult> Update(int workId, [FromBody]WorkStatusConfirmationDto workStatusConfirmation)
        {
            await _activitiesService.Update(workStatusConfirmation);

            return NoContent();
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
        public async Task<IActionResult> Update(int id, [FromBody] NewActivityDto newActivityDto)
        {
            await _activitiesService.Update(id, newActivityDto);

            return NoContent();
        }
    }
}
