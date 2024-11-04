namespace projekat_kaja.DTOs;

public class ReviewDTO
{
    public int Id { get; set; }
    public double Ocena { get; set; }
    public string? Komentar { get; set; }
    public string? User { get; set; }
    public string? Event { get; set; }
}
