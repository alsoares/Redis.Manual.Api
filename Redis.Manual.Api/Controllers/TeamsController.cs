using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redis.Manual.Api.Models;

namespace Redis.Manual.Api.Controllers
{
    [ApiController()]
    [Route("api/[Controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly Cache.Cache _cache;

        public TeamsController(Cache.Cache cache)
        {
            _cache = cache;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {

            return Ok(await _cache.Get<Team>(name));

        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _cache.Delete(name);

            return Ok();

        }

         [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody]Team team)
        {
            await _cache.Set(name , team, -1);
            return Ok();

        }

    }
}