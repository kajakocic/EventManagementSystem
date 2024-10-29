using Microsoft.AspNetCore.Mvc;
using projekat_kaja.DTOs;
using projekat_kaja.Models;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locService;

    public LocationController(ILocationService locService)
    {
        _locService = locService;
    }

    [Route("DodajLokaciju")]
    [HttpPost]
    public IActionResult AddLocation([FromBody] Location loc)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _locService.AddLoc(loc);
            return Ok($"Lokacija: {loc.Naziv} je dodata!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziSveLokacije")]
    [HttpGet]
    public IActionResult GetLocations()
    {
        try
        {
            return Ok(_locService.GetAllLoc().Select(l =>
            new LocationDTO
            {
                ID = l.ID,
                Naziv = l.Naziv,
            }).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziLokaciju/{id}")]
    [HttpGet]
    public IActionResult GetLocation(int id)
    {
        try
        {
            var l = _locService.GetLocById(id);
            return Ok(new LocationDTO
            {
                ID = l.ID,
                Naziv = l.Naziv
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ObrisiLokaciju/{id}")]
    [HttpDelete]
    public IActionResult DeleteLoc(int id)
    {
        try
        {
            _locService.DeleteLoc(id);
            return Ok($"Lokacija id: {id} je uklonjena.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}