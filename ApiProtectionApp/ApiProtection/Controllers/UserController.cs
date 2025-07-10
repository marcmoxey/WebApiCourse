using ApiProtection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProtection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IEnumerable<string> Get()
        {
            return new string[] { Random.Shared.Next(1, 101).ToString() };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public string Get(int id)
        {
            return $"Random Number: { Random.Shared.Next(1,10)} for Id { id }";
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if(ModelState.IsValid)
            {
                return Ok("The modle is valid");
            } else
            {
                return BadRequest();
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
