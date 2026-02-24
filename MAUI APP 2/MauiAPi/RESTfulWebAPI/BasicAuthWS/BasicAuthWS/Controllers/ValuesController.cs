using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasicAuthWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [BasicAuthentication] // Apply the BasicAuthentication filter to this action
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
