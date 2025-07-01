using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers;

[Route("api/v{verion:apiVersion}/[controller]")]
[ApiController]
[ApiVersionNeutral]
public class HealthController : ControllerBase
{
    // check health of overall API

    [HttpGet]
    [Route("ping")]
    public IActionResult Ping()
    {
        return Ok("Everything seems greate!");
    }
}
