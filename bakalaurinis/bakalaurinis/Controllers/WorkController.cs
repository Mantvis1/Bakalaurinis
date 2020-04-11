using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bakalaurinis.Dtos.Work;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController : ControllerBase
    {
        private readonly IWorksService _worksService;
        public WorkController(IWorksService worksService)
        {
            _worksService = worksService;
        }

        [HttpPost]
        [Produces(typeof(int))]
        public async Task<IActionResult> Create([FromBody] NewWorkDto newActivityDto)
        {
            var newActivityId = await _worksService.Create(newActivityDto);

            return Ok(newActivityId);
        }

        [HttpGet]
        [Produces(typeof(WorkDto[]))]
        public async Task<IActionResult> Get()
        {
            var activities = await _worksService.GetAll();

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpGet("{id}")]
        [Produces(typeof(WorkDto))]
        public async Task<IActionResult> Get(int id)
        {
            var activity = await _worksService.GetById(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpGet("user/{userId}")]
        [Produces(typeof(WorkDto[]))]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var activities = await _worksService.GetByUserId(userId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _worksService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewWorkDto newActivityDto)
        {
            await _worksService.Update(id, newActivityDto);

            return NoContent();
        }
    }
}
