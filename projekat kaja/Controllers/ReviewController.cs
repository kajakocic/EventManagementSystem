using Microsoft.AspNetCore.Mvc;
using projekat_kaja.DTOs;
using projekat_kaja.Services;

namespace projekat_kaja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [Route("DodajReview")]
    [HttpPost]
    public IActionResult AddReview([FromBody] ReviewDTO re)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _reviewService.AddReview(re);
            return Ok("Review je dodat!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /* [Route("prikaziReviews/{id}")]
    [HttpGet]
    public IActionResult GetAllEvents(int id)
    {
        try
        {
            return Ok(_eventService.GetAllEvents());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } */
}