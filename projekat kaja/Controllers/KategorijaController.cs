using Microsoft.AspNetCore.Mvc;
using projekat_kaja.Models;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KategorijaController : ControllerBase
{
    private readonly IKategorijaService _katService;

    public KategorijaController(IKategorijaService katService)
    {
        _katService = katService;
    }

    //metode
    [Route("DodajKategoriju")]
    [HttpPost]
    public IActionResult AddKategorija([FromBody] Kategorija kat)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var addedEvent = _katService.AddKat(kat);
            return Ok("Kategorija je dodata!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PreuzmiKategorije")]
    [HttpGet]
    public IActionResult GetAllKategorije()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            return Ok(_katService.GetAllKat().ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //nije dovrsena metoda sta ako taj id ne postoji
    [HttpDelete("{id}")]
    public IActionResult ObrisiEvent(int id)
    {
        try
        {
            _katService.DeleteKat(id);
            return Ok("Kategorija je uklonjena.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}