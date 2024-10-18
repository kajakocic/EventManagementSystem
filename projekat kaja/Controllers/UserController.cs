using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;

namespace projekat_kaja.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private EMSContext Context { get; set; }

    public UserController(EMSContext context)
    {
        Context = context;
    }

    //metode
}