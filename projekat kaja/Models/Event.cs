using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Event
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        [Required]
        public string? Naziv { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Vreme { get; set; }
        public string? Kategorija { get; set; }
        public string? Opis { get; set; }
        public string? URLimg { get; set; }

    }
}