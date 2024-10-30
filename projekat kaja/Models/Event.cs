using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace projekat_kaja.Models;

[Table("EVENT")]
public class Event
{
    [Key]
    public int ID { get; set; }
    [MaxLength(50)]
    [Required]
    public string? Naziv { get; set; }
    [Required]
    public DateTime Datum { get; set; }
    [Required]
    [Range(0, 100000)]
    public int Kapacitet { get; set; }
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
    [JsonIgnore]
    public Kategorija? KategorijaEvent { get; set; }
    [JsonIgnore]
    public Location? LokacijaEvent { get; set; }
    public List<Review>? ReviewsEvent { get; set; }
}