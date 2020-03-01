using bakalaurinis.Dtos.Activity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {  
        public SettingsController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewActivityDto newActivityDto)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Produces(typeof(ActivityDto))]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            return Ok(id);
        }
    }
}
