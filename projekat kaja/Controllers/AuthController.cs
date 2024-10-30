using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using projekat_kaja.DTOs;
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

    [Route("Register")]
    [HttpPost]
    public IActionResult Register(UserDto request)
    {
        var postojeciUser = UserService.GetByEmail(request.Email);
        if (postojeciUser != null)
        {
            return BadRequest("Ovaj nalog već postoji.");
        }
        try
        {
            var korisnik = new User
            {
                Ime = request.Ime,
                Prezime = request.Prezime,
                Password = request.Password,
                Email = request.Email,
                Tip = 0
            };

            var kreirani = UserService.RegisterUser(korisnik);
            return Ok(kreirani);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var korisnik = UserService.GetByEmail(loginRequest.Email);
        if (korisnik == null)
        {
            return BadRequest("Korisnik nije pronadjen.");
        }
        if (korisnik.Password != loginRequest.Password)
        {
            return BadRequest("Neispravan password.");
        }
        try
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, korisnik.Email),
                new Claim(ClaimTypes.Role, korisnik.Tip.ToString())
            };
            //var user = UserService.ValidateUser(loginRequest.Email, loginRequest.Password);
            /*  if (user == null)
                 return Unauthorized("Neispravni podaci za prijavu."); */

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
              Config["Jwt:Issuer"],
              Config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Route("DecodeToken")]
    [HttpGet]
    public IActionResult DecodeToken(string jwtToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config["Jwt:Key"]);
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtTokenDecoded = (JwtSecurityToken)validatedToken;

            var email = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var userType = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            return Ok(new DecodedToken
            {
                UserType = userType
            });
        }
        catch (Exception ex)
        {
            return BadRequest("Neispravan token: " + ex.Message);
        }
    }

    [Route("GetUserByToken")]
    [HttpGet]
    public IActionResult GetUserByToken(string jwtToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config["Jwt:Key"]);
            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtTokenDecoded = (JwtSecurityToken)validatedToken;

            var email = jwtTokenDecoded.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            var user = UserService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("Korisnik nije pronadjen.");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest("Neispravan token: " + ex.Message);
        }
    }

    public class LoginRequest()
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class DecodedToken
    {
        public string? Email { get; set; }
        public string? UserType { get; set; }
    }
}