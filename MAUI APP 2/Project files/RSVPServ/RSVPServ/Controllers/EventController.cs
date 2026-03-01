using Microsoft.AspNetCore.Mvc;
using RSVPServ.Controllers;
using RSVPServ.Models;

namespace RSVPServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BasicAuthentication]
    public class EventController : ControllerBase
    {
        private static List<Event> _events = new List<Event>();

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _events;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Event newEvent)
        {
            if (newEvent == null) return BadRequest();

            _events.Add(newEvent);
            return Ok(new { message = "Event saved to RSVPServ!" });
        }
    }
}