using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    public EMSContext Context { get; set; }

    public EventController(EMSContext context)
    {
        Context = context;
    }

    //metode
    [HttpPost]
    public async Task<ActionResult<Event>> AddEvent([FromBody] Event dogadjaj)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            Context.Events.Add(dogadjaj);
            await Context.SaveChangesAsync();

            //da li treba da se prikazuje id? ako ne sta drugo da vratim
            //return Ok($"Dogadjaj je dodat. ID:{dogadjaj.ID}");
            return CreatedAtAction(nameof(AddEvent), new { id = dogadjaj.ID }, dogadjaj);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEvents")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        try
        {
            return await Context.Events.ToListAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEventByID/{id}")]
    //[HttpGet("{id}")]
    [HttpGet]
    //da li da dodajem produces responce type
    /* [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]  
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]*/
    public async Task<ActionResult<Event>> GetEventByID(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Neispravan ID.");
        }
        try
        {
            //dali ovde treba da filtriram reviews po oceni? akjo ne ovde gde?
            var ev = await Context.Events.FindAsync(id);

            if (ev == null)
            {
                return NotFound();
            }

            return ev;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("SortEvents")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> SortEvents(
            DateTime? datum = null,
            TimeSpan? vreme = null,
            string? kategorija = null,
            string? lokacija = null)
    {
        try
        {
            var query = Context.Events.AsQueryable();

            if (datum.HasValue)
            {
                query = query.Where(e => e.Datum.Date == datum.Value.Date);
            }
            if (vreme.HasValue)
            {
                query = query.Where(e => e.Vreme == vreme.Value);
            }
            //da li su dobre kategorija i lokacija
            if (!string.IsNullOrWhiteSpace(kategorija))
            {
                query = query.Where(e => e.KategorijaEvent != null
                                      && e.KategorijaEvent.Naziv == kategorija);
            }
            if (!string.IsNullOrWhiteSpace(lokacija))
            {
                query = query.Where(e => e.LocationEvent != null
                                      && e.LocationEvent.Naziv == lokacija);
            }
            //kako da implementiram sortiranje po ceni? ovde ili na frontendu
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Event>> IzmeniEvent([FromBody] Event ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            Context.Events.Update(ev);
            await Context.SaveChangesAsync();
            //return Ok($"Event je izmenjen. ID:{ev.ID}");
            return CreatedAtAction(nameof(IzmeniEvent), new { id = ev.ID }, ev);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> ObrisiEvent(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Neispravan ID.");
        }
        try
        {
            var obrisi = await Context.Events.FindAsync(id);
            var nazivObrisanog = "";
            if (obrisi != null)
            {
                nazivObrisanog = obrisi.Naziv;
                Context.Events.Remove(obrisi);
            }
            await Context.SaveChangesAsync();
            return Ok($"{nazivObrisanog} je uklonjen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}