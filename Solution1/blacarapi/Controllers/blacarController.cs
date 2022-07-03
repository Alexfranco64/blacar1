using blacarapi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blacarapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class blacarController : ControllerBase
    {
        // GET: api/<blacarController>
        [HttpGet]
        public ApiResponse Get()
        {
            return new Blamodel().GetAll();
        }

        // GET api/<blacarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<blacarController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<blacarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<blacarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
