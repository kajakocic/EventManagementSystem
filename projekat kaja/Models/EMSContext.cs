using Microsoft.EntityFrameworkCore;

namespace projekat_kaja.Models;

public class EMSContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Kategorija> Kategorije { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public EMSContext(DbContextOptions options) : base(options)
    {

    }
}