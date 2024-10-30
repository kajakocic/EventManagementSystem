using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepositoriy
{
    public EventRepository(EMSContext context) : base(context)
    {
    }

    public override Event Update(Event x)
    {
        var ev = _context.Events.Single(e => e.ID == x.ID);

        ev.Naziv = x.Naziv;
        ev.Datum = x.Datum;
        ev.Opis = x.Opis;
        ev.CenaKarte = x.CenaKarte;
        ev.URLimg = x.URLimg;
        ev.KategorijaEvent.ID = x.KategorijaEvent.ID;
        ev.LokacijaEvent.ID = x.LokacijaEvent.ID;


        return base.Update(ev);
    }

    public override Event Get(int id)
    {
        _context.Events
            .Include(k => k.KategorijaEvent)
            .Include(l => l.LokacijaEvent)
            .FirstOrDefault(e => e.ID == id);

        return base.Get(id);
    }

    //mtoda koja pretrazuje sve evente koje se jedan user prijavio
    /* return _context.Events
            .Include(r => r.UsersEvent)
            .ThenInclude(u => u.UserEvent)
            .Where(x)
            .ToList(); */
    public override IEnumerable<Event> Find(Expression<Func<Event, bool>> predicate)
    {
        return _context.Events
                        .Include(k => k.KategorijaEvent)
                        .Include(l => l.LokacijaEvent)
                        .Where(predicate)
                        .ToList();
    }
}