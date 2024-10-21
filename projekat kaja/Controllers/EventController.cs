using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IRepository<Event> eventRepository;

    public EventController(IRepository<Event> eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    //metode
    //prikazuje dogadjaje koji su se desili u poslednja 24h ili tek treba da se dogode
    /* public IActionResult Index()
    {
        var ev = eventRepository
            .Find(e => e.Datum > DateTime.UtcNow.AddDays(-1));
        return Ok(ev);
    } */

    [HttpPost]
    public IActionResult AddEvent([FromBody] Event dogadjaj)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            eventRepository.Add(dogadjaj);
            eventRepository.SaveChanges();

            //da li treba da se prikazuje id? ako ne sta drugo da vratim
            return Ok($"Dogadjaj je dodat. ID:{dogadjaj.ID}");
            //return CreatedAtAction(nameof(AddEvent), new { id = dogadjaj.ID }, dogadjaj);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEvents")]
    [HttpGet]
    public IActionResult GetEvents()
    {
        try
        {
            var evs = eventRepository.GetAll();
            return Ok(evs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEventByID/{id}")]
    //[HttpGet("{id}")]
    [HttpGet]
    public IActionResult GetEventByID(Guid id)
    {
        try
        {
            //dali ovde treba da filtriram reviews po oceni? akjo ne ovde gde?
            var ev = eventRepository.Get(id);

            if (ev == null)
            {
                return NotFound();
            }

            return Ok(ev);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /*[Route("SortEvents")]
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
    }*/

    [HttpPut]
    public IActionResult IzmeniEvent([FromBody] Event ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            eventRepository.Update(ev);
            eventRepository.SaveChanges();
            return Ok($"Event je izmenjen. ID:{ev.ID}");
            //return CreatedAtAction(nameof(IzmeniEvent), new { id = ev.ID }, ev);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult ObrisiEvent(Guid id)
    {
        try
        {
            eventRepository.Delete(id);
            eventRepository.SaveChanges();
            return Ok("Event je uklonjen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}