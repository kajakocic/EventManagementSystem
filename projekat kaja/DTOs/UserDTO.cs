namespace projekat_kaja.DTOs;

public class UserDto
{
    public int ID { get; set; }
    public string? Ime { get; set; }  //kako da se resim ovog warninga za null reference?
    public string? Prezime { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}