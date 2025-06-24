using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GettingStartedApi.Controllers;

// GET, POST, PUT, PATCH, DELETE 


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        List<string> output = new();
        for(int i = 0; i < Random.Shared.Next(2,10); i++)
        {
            output.Add($"Value #{i + 1}");
        }

        return output;

        //return new string[] { "value1", "value2" };
    }

    // GET api/Users/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"Value #{id+1}";
    }

    // POST creates a new record
    // POST api/Users
    // https://localhost:7295/api/Users (Post)
    [HttpPost]
    public void Post([FromBody] string value)
    {
        // FromBody holds the data
    }

    // PUT updates a record
    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        // FN, LN, Email, PhoneNumber
    }


    // PATCH updates part of a record 
    // PATCH api/User/5
    [HttpPatch("{id}")]
    public void Patch(int id, [FromBody] string emailAddress)
    {

    }


    // PATCH api/User/Email/5
    //[HttpPatch("Email/{id}")]
    //public void Patch(int id, [FromBody] string emailAddress)
    //{

    //}


    // DELETE deletes a record 
    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
