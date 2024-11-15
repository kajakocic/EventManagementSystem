using Microsoft.AspNetCore.Mvc;
using projekat_kaja.DTOs;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    //metode
    [Route("AddReservation")]
    [HttpPost]
    public IActionResult DodajRez([FromBody] AddRegDTO reservationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _registrationService.AddReservation(reservationDto);

            var novaRezervacija = new RegistrationDTO
            {
                UserId = result.UserEvent.ID,
                UserName = result.UserEvent.Email,
                EventId = result.EventUser.ID,
                EventName = result.EventUser.Naziv,
                BrMesta = result.BrojMesta
            };

            return Ok(novaRezervacija);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("DodajRezervaciju/{eventId}/{UserId}")]
    [HttpPost]
    public IActionResult AddReservation(int eventId, int UserId, int brMesta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _registrationService.MakeReservation(eventId, UserId, brMesta);

            var novaRezervacija = new RegistrationDTO
            {
                UserId = result.UserEvent.ID,
                UserName = result.UserEvent.Email,
                EventId = result.EventUser.ID,
                EventName = result.EventUser.Naziv,
                BrMesta = brMesta
            };

            return Ok(novaRezervacija);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziRezervaciju/{id}")]
    [HttpGet]
    public IActionResult GetReservation(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _registrationService.GetReservation(id);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PrikaziRezervacije/{userId}")]
    [HttpGet]
    public IActionResult Getreservations(int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = _registrationService.GetReservations(userId);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ObrisiRezervaciju/{id}")]
    [HttpDelete]
    public IActionResult DeleteReservation(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _registrationService.DeleteReservation(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}