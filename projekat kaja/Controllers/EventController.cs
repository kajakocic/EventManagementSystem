using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;
using projekat_kaja.Services;
namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService EventService;

    public EventController(IEventService eventService)
    {
        EventService = eventService;
    }

    //metode
    [HttpPost]
    public IActionResult AddEvent([FromBody] Event ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var addedEvent = EventService.AddEvent(ev);
            return CreatedAtAction(nameof(EventService.GetEventById), new { id = addedEvent.ID }, addedEvent);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEvents")]
    [HttpGet]
    public IActionResult GetEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null)
    {
        if (!datum.HasValue && !vreme.HasValue && string.IsNullOrWhiteSpace(kategorija) && string.IsNullOrWhiteSpace(lokacija))
        {
            return BadRequest("Odaberi parametar za filtriranje.");
        }
        try
        {
            var evs = EventService.FilterEvents(datum, vreme, kategorija, lokacija);
            return Ok(evs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("GetEvent/{id}")]
    //[HttpGet("{id}")]
    [HttpGet]
    public IActionResult GetEvent(int id)
    {
        try
        {
            var ev = EventService.GetEventById(id);

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

    [HttpPut]
    public IActionResult IzmeniEvent([FromBody] Event ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var ue = EventService.UpdateEvent(ev);
            return CreatedAtAction(nameof(EventService.GetEventById), new { id = ue.ID }, ue);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult ObrisiEvent(int id)
    {
        try
        {
            EventService.DeleteEvent(id);
            return Ok("Event je uklonjen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}