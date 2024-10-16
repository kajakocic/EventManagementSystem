using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models
{
    [Table("LOKACIJE")]
    public class Location
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(70)]
        public string? Naziv { get; set; }

        //veze
        public List<Event>? EventsLocation { get; set; }
    }
}