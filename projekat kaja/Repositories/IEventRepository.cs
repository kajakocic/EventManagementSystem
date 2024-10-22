using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public interface IEventRepository : IRepository<Event>
{
    IEnumerable<Event> FilterAllEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null);
}