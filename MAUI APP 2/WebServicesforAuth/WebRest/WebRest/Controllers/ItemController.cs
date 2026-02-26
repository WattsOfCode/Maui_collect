using WebRest.DataAccess;
using WebRest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return new ItemData().GetItem();
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            var pd = new ItemData();
            pd.SaveItem(value);
        }
    }
}
