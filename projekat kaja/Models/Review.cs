using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekat_kaja.Models;

[Table("REVIEW")]
public class Review
{
    [Key]
    public int ID { get; set; }
    [Required]
    [Range(1, 5)]
    public double Ocena { get; set; }
    [MaxLength(2000)]
    public string? Komentar { get; set; }

    //veze
    public User? UserReview { get; set; }
    public Event? EventReview { get; set; }
}