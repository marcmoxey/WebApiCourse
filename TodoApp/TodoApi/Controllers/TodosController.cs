using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;

    public TodosController(ITodoData data)
    {
        _data = data;
    }
    // GET: api/Todos
    // ActionResult - return differnt types of values 
    [HttpGet]
    public ActionResult<IEnumerable<TodoModel>> Get()
    {
        // look up id
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
       _data.GetAllAssigned(int.Parse(userId));
    }

    // GET api/Todos/5
    [HttpGet("{id}")]
    public ActionResult<TodoModel> Get(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/Todos
    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        throw new NotImplementedException();
    }

    // PUT api/Todos/5
    [HttpPut("{id}/Complete")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    // PUT api/Todos/5/Complete
    [HttpPut]
    public IActionResult Complete(int id)
    {
        throw new NotImplementedException();
    }

    // DELETE api/Todos/5
    [HttpDelete("{id}")]
    public IActionResult  Delete(int id)
    {
        throw new NotImplementedException();
    }
}
