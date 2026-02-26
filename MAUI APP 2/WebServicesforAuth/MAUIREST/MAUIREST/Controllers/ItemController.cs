using MAUIREST.DataAccess;
using MAUIREST.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MauiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return new ItemData().GetItem();
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            var pd = new ItemData();
            pd.SaveItem(value);
        }
    }
}
