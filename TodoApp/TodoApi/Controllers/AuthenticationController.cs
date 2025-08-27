using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    public record AuthenticationData(string? Username, string? Password);
    public record UserData(int Id, string FirstName, string LastName, string UserName );

    // Auth Method 
    [HttpPost("token")]
    [AllowAnonymous] 
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);

        if(user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            _config.GetValue<string>("Authentication:SecretKey")));

        var signingCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256 );

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName.ToString()));

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(1),
           signingCredential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
       // THIS IS NOT PRODUCTION CODE - REPLACE THIS WITH A CALL TO YOUR AUTH SYSTEM
       if(CompareValues(data.Username, "mmoxey") && 
          CompareValues(data.Password, "Test123")) 
        {
            return new UserData(1, "Marc", "Moxey", data.Username!);
        }

        if (CompareValues(data.Username, "tcorey") &&
         CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, "Tim", "Corey", data.Username!);
        }

        return null;    
    }

    private bool CompareValues(string? actual, string expected)
    {
        if(actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }

        return false; 
    }
}
