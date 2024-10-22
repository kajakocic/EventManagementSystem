using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using projekat_kaja.Models;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService UserService;
    private readonly IConfiguration Config;

    public AuthController(IUserService userService, IConfiguration config)
    {
        UserService = userService;
        Config = config;
    }

    [HttpPost]
    public IActionResult Login([FromBody] User loginRequest)
    {
        //var user = UserService.ValidateUser(loginRequest.Email, loginRequest.Password);
       /*  if (user == null)
            return Unauthorized("Neispravni podaci za prijavu."); */

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(
          Config["Jwt:Issuer"],
          Config["Jwt:Issuer"],
          null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return Ok(token);
    }
}