using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(EMSContext context) : base(context)
    {
    }

    public override Event Update(Event x)
    {
        var ev = Context.Events.Single(e => e.ID == x.ID);

        ev.Naziv = x.Naziv;
        ev.Datum = x.Datum;
        ev.Vreme = x.Vreme;
        ev.Opis = x.Opis;
        ev.CenaKarte = x.CenaKarte;
        ev.URLimg = x.URLimg;
        if (ev.KategorijaEvent != null && x.KategorijaEvent != null)
        {
            ev.KategorijaEvent.ID = x.KategorijaEvent.ID;
        }
        if (ev.LocationEvent != null && x.LocationEvent != null)
        {
            ev.LocationEvent.ID = x.LocationEvent.ID;
        }

        return base.Update(ev);
    }

    //mtoda koja pretrazuje sve evente koje se jedan user prijavio
    public override IEnumerable<Event> Find(Expression<Func<Event, bool>> x)
    {
        return Context.Events
            .Include(r => r.UsersEvent)
            .ThenInclude(u => u.UsersEvents)
            .Where(x)
            .ToList();
    }

    public IEnumerable<Event> FilterAllEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null)
    {
        var query = Context.Events.AsQueryable();

        if (datum.HasValue)
        {
            query = query.Where(e => e.Datum.Date == datum.Value.Date);
        }
        if (vreme.HasValue)
        {
            query = query.Where(e => e.Vreme == vreme.Value);
        }
        if (!string.IsNullOrWhiteSpace(kategorija))
        {
            query = query.Where(e => e.KategorijaEvent != null && e.KategorijaEvent.Naziv == kategorija);
        }
        if (!string.IsNullOrWhiteSpace(lokacija))
        {
            query = query.Where(e => e.LocationEvent != null && e.LocationEvent.Naziv == lokacija);
        }

        return query.ToList();
    }
}