namespace projekat_kaja.DTOs;
public class EventDTO
{
    public int ID { get; set; }
    public string? Naziv { get; set; }
    public DateTime Datum { get; set; }
    public TimeSpan Vreme { get; set; }
    public string? Opis { get; set; }
    public string? URLimg { get; set; }

    public int KategorijaID { get; set; }
    public int LokacijaID { get; set; }
}