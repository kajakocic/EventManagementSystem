using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models
{
    [Table("KATEGORIJA")]
    public class Kategorija
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Naziv { get; set; }

        //veze
        public List<Event>? EventspoKateg { get; set; }
    }
}