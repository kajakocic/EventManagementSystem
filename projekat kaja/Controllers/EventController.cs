using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;

namespace projekat_kaja.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    public EMSContext Context { get; set; }

    public EventController(EMSContext context)
    {
        Context = context;
    }

    //metode
    [Route("AddEvent")]
    [HttpPost]
    public async Task<ActionResult> AddEvent([FromBody] Event dogadjaj)
    {
        if (string.IsNullOrWhiteSpace(dogadjaj.Naziv) || dogadjaj.Naziv.Length > 50)
        {
            return BadRequest("Neispravan naziv dogadjaja.");
        }
        if (dogadjaj.Datum < DateTime.Now)
        {
            return BadRequest("Neispravan datum.");
        }
        if (string.IsNullOrWhiteSpace(dogadjaj.Opis))
        {
            return BadRequest("Opis doogadjaja je obavezan.");
        }
        if (dogadjaj.CenaKarte < 0 || dogadjaj.CenaKarte > 100000)
        {
            return BadRequest("Cena karte ide do 100 000din, karta takodje moze biti besplatna.");
        }
        if (string.IsNullOrWhiteSpace(dogadjaj.URLimg) || !Uri.IsWellFormedUriString(dogadjaj.URLimg, UriKind.Absolute))
        {
            return BadRequest("Neispravan URL slike.");
        }

        try
        {
            Context.Events.Add(dogadjaj);
            await Context.SaveChangesAsync();

            return Ok($"Dogadjaj je dodat. ID:{dogadjaj.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}