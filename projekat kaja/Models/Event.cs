using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models;

[Table("EVENT")]
public class Event
{
    [Key]
    public int ID { get; set; }
    [MaxLength(50)]
    [Required]
    public string? Naziv { get; set; }
    [DataType(DataType.Date)]
    [Required]
    public DateTime Datum { get; set; }
    [Required]
    public TimeSpan Vreme { get; set; }
    [Required]
    public string? Opis { get; set; }
    [Range(0, 100000)]
    [Required]
    public double CenaKarte { get; set; }
    [Url]
    [Required]
    public string? URLimg { get; set; }

    //veze
    public List<Registration>? UsersEvent { get; set; }
    public Kategorija? KategorijaEvent { get; set; }
    public Location? LocationEvent { get; set; }
    public List<Review>? ReviewsEvent { get; set; }
}