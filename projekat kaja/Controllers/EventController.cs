using Microsoft.AspNetCore.Mvc;
using projekat_kaja.DTOs;
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

    [Route("DodajEvent")]
    [HttpPost]
    public IActionResult AddEvent([FromBody] AddEventDTO ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _eventService.AddEvent(ev);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("prikaziEvente")]
    [HttpGet]
    public IActionResult GetAllEvents()
    {
        try
        {
            return Ok(_eventService.GetAllEvents());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /* [Route("PrikaziFiltriraneEvente")]
    [HttpGet]
    public IActionResult GetEvents(DateTime? datum = null, string? kategorija = null, string? lokacija = null)
    {
        // if (!datum.HasValue && string.IsNullOrWhiteSpace(kategorija) && string.IsNullOrWhiteSpace(lokacija))
        // {
        //     return BadRequest("Odaberi parametar za filtriranje.");
        // }
        try
        {
            var filtriraniEv = _eventService.FilterEvents(datum, kategorija, lokacija);
            return Ok(filtriraniEv);
             return Ok(_eventService.FilterEvents(datum, kategorija, lokacija).Select(ev => new EventDTO
             {
                 Naziv = ev.Naziv,
                 Datum = ev.Datum,
                 Opis = ev.Opis,
                 CenaKarte = ev.CenaKarte,
                 Kategorija = ev.KategorijaEvent.Naziv,
                 Lokacija = ev.LokacijaEvent.Naziv
             })); 
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } */

    /* [Route("PrikaziEvent/{id}")]
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

            return Ok(new EventDTO
            {
                Naziv = ev.Naziv,
                Datum = ev.Datum,
                Opis = ev.Opis,
                CenaKarte = ev.CenaKarte,
                Kategorija = ev.KategorijaEvent.Naziv,
                Lokacija = ev.LokacijaEvent.Naziv
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } */

    [Route("izmeniEvent")]
    [HttpPut]
    public IActionResult IzmeniEvent([FromBody] AddEventDTO ev)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var ue = _eventService.UpdateEvent(ev);
            return Ok(ue);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /* 
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
        } */

    [Route("ObrisiEvent/{id}")]
    [HttpDelete]
    public IActionResult ObrisiEvent(int id)
    {
        try
        {
            _eventService.DeleteEvent(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}