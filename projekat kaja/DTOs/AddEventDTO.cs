namespace projekat_kaja.DTOs;

public class AddEventDTO
{
    public int Id { get; set; }
    public string? Naziv { get; set; }
    public DateTime Datum { get; set; }
    public int Kapacitet { get; set; }
    public string? Opis { get; set; }
    public double CenaKarte { get; set; }
    public string? URLimg { get; set; }
    public int KategorijaId { get; set; }
    public int LokacijaId { get; set; }
}