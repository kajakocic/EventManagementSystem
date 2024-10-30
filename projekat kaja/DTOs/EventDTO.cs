using projekat_kaja.Models;

namespace projekat_kaja.DTOs;
public class EventDTO
{
    public string? Naziv { get; set; }
    public DateTime Datum { get; set; }
    public int Kapacitet { get; set; }
    public string? Opis { get; set; }
    public double CenaKarte { get; set; }
    public string? URLimg { get; set; }
    public Kategorija? Kategorija { get; set; }
    public Location? Lokacija { get; set; }
}