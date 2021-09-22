using DistributedCache.API.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace DistributedCache.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<ActionResult<Student>> GetStudent([FromServices] IConnectionMultiplexer connectionMultiplexer, [FromRoute] string name)
        {
            var student = await connectionMultiplexer.GetDatabase().StringGetAsync(name);
            if (!student.HasValue)
                return NoContent();

            return Ok(JsonSerializer.Deserialize<Student>(student));
        }

        [HttpPost]
        public async Task<ActionResult> PostStudent([FromServices] IConnectionMultiplexer connectionMultiplexer, [FromBody] Student student)
        {
            await connectionMultiplexer.GetDatabase()
                .StringSetAsync(new RedisKey(student.Name), JsonSerializer.Serialize(student));

            return Ok();
        }
    }
}