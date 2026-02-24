using MauiWebApi.DataAccess;
using MauiWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MauiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return new PersonData().GetPeople();
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] Person value)
        {
            var pd = new PersonData();
            pd.SavePerson(value);
        }
    }
}
