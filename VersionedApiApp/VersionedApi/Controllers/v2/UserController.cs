using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VersionedApi.Controllers.v2;

// api/v2/Users
[Route("api/v2/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class UserController : ControllerBase
{
    // GET: api/<UserController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Version 2 value1", "Version 2 value2" };
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<UserController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
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
