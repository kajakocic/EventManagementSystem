using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models;

[Table("REGISTRACIJA")]
public class Registration
{
    [Key]
    public int ID { get; set; }

    //veze
    [Required]
    public User? UsersEvents { get; set; }
    [Required]
    public Event? EventsUsers { get; set; }
}
