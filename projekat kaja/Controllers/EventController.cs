using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
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
            var addedEvent = _eventService.AddEvent(ev);
            return CreatedAtAction(nameof(_eventService.GetEventById), new { id = addedEvent.ID }, addedEvent);
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
            var filtriraniEv = _eventService.FilterEvents(datum, vreme, kategorija, lokacija);
            return Ok(filtriraniEv);
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
            var ev = _eventService.GetEventById(id);

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
            var ue = _eventService.UpdateEvent(ev);
            return CreatedAtAction(nameof(_eventService.GetEventById), new { id = ue.ID }, ue);

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
            _eventService.DeleteEvent(id);
            return Ok("Event je uklonjen.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziReviews/{eventid}")]
    [HttpGet]
    public IActionResult PrikaziReviews(int eventid)
    {
        try
        {
            return Ok(_eventService.GetReviews(eventid));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}