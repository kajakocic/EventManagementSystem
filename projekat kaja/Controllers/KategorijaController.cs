using Microsoft.AspNetCore.Mvc;
using projekat_kaja.DTOs;
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
            _katService.AddKat(kat);
            return Ok($"Kategorija: {kat.Naziv} je dodata!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziSveKategorije")]
    [HttpGet]
    public IActionResult GetAllKat()
    {
        try
        {
            return Ok(_katService.GetAllKat().Select(k =>
            new KategorijaDTO
            {
                ID = k.ID,
                Naziv = k.Naziv
            }).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Route("ObrisiKategoriju/{id}")]
    [HttpDelete]
    public IActionResult DeleteKategorija(int id)
    {
        try
        {
            _katService.DeleteKat(id);
            return Ok($"Kategorija id: {id} je uklonjena.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}