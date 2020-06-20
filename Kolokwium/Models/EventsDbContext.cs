using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Models
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artist_Event> Artist_Events { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Event_Organiser> Event_Organisers { get; set; }
        public DbSet<Organiser> Organisers { get; set; }

        public EventsDbContext()
        {
        }

        public EventsDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
