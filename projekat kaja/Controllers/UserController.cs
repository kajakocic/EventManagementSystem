using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;

namespace projekat_kaja.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public EMSContext Context { get; set; }

    public UserController(EMSContext context)
    {
        Context = context;
    }

    //metode
}