using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    /* public EMSContext Context { get; set; }
    public AuthController(EMSContext context)
    {
        Context = context;
    } */
    private IConfiguration _config;
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost]
    public IActionResult Post([FromBody] LoginRequest loginRequest)
    {
        //your logic for login process
        //If login usrename and password are correct then proceed to generate token


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return Ok(token);
    }
}