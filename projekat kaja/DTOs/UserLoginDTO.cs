using projekat_kaja.Models;

namespace projekat_kaja.DTOs;

public class UserLoginDto
{
    public int ID { get; set; }
    public string? Ime { get; set; }
    public string? Prezime { get; set; }
    public string? Email { get; set; }
    public TipKorisnika Tip { get; set; }
    public string? Token { get; set; }
}