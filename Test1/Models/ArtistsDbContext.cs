using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class ArtistsDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<EventOrganiser> EventOrganisers { get; set; }
        public DbSet<ArtistEvent> ArtistEvents { get; set; }


        public ArtistsDbContext()
        {
        }

        public ArtistsDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistEvent>()
                .HasKey(c => new { c.idArtist, c.idEvent }); //doubleKey

            modelBuilder.Entity<EventOrganiser>()
                .HasKey(c => new { c.idEvent, c.idOrganiser }); //doubleKey

        }

    }
}
