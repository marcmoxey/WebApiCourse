using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSecurtiy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config; 

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }
    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName);


    // api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        // validate Credentials
        var user = ValidateCredentials(data);
        if (user is null)
        {
            return Unauthorized();
        }

        // return a token
        var token = GenerateToken(user);
        return Ok(token);

    }
    private string GenerateToken(UserData user)
    {
        // Convert string ASII byte array
        // Gets SecretKey from secreats.json
        // ':' helps go level deeper if needed
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey"))); 


        // signed token
        // take token and sign it with secret key 
        // if anything changes inside a token it fails 
        var signingCredentails = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));


        // build token
        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow, // When the token becomes valid
            DateTime.UtcNow.AddMinutes(1),// when the token will expire
            signingCredentails); // sign the token

        // create token 
        return new JwtSecurityTokenHandler().WriteToken(token);
            
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // THIS NOT PRODUCTION CODE = THIS IS ONLY A DEMO - DO NOT USE IN REAL LIFE
        if(CompareValues(data.UserName, "mmoxey") && CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, data.UserName!); 
        }

        if (CompareValues(data.UserName, "tcorey") && CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.UserName!);
        }

        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        if(actual != null)
        {
            if(actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }
}
