using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models;

public enum TipKorisnika
{
    Admin,
    RegularUser
}

[Table("USER")]
public class User
{
    [Key]
    public int ID { get; set; }
    [MaxLength(40)]
    [Required]
    public string? Ime { get; set; }
    [MaxLength(40)]
    [Required]
    public string? Prezime { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Neispravna lozinka.")]
    public string? Password { get; set; }
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Neispravna email adresa.")]
    public string? Email { get; set; }

    public TipKorisnika Tip { get; set; }

    //veze
    public List<Registration>? EventsUsers { get; set; }
    public List<Review>? ReviewsUser { get; set; }
}