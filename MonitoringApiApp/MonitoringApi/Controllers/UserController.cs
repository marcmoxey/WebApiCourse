using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MonitoringApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //if(id < 0 || id > 100)
            //{
            //    _logger.LogWarning("The give id of {Id} was invalid.", id);
            //    return BadRequest("The index was out of range");
            //}
            //_logger.LogInformation(@"The api\Users\{id} was called", id);
            //return Ok($"Value {id}");

            // if logging cant be done synchronous you doing logging wrong
            try
            {
                if (id < 0 || id > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(id));
                }

                _logger.LogInformation(@"The api\Users\{id} was called", id);
                return Ok($"Value {id}");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,"The give id of {Id} was invalid.", id);
                return BadRequest("The index was out of range");
            }
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
}
