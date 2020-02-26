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
      
        public ScheduleController()
        {
        }


        [HttpGet("{id}")]
        [Produces(typeof(ActivityDto))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok();
        }

    }
}
