using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class EventRepository : GenericRepository<Event>
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
        ev.KategorijaEvent.ID = x.KategorijaEvent.ID;
        ev.LocationEvent.ID = x.LocationEvent.ID;


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

}