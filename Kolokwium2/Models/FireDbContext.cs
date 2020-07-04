using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{
    public class FireDbContext : DbContext
    {
        public DbSet<Action> Actions { get; set; }
        public DbSet<Firefighter> Firefighters { get; set; }
        public DbSet<FireTruck> FireTrucks { get; set; }
        public DbSet<FirefighterAction> FirefighterActions { get; set; }
        public DbSet<FireTruckAction> FireTruckActions { get; set; }


        public FireDbContext()
        {
        }

        public FireDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FirefighterAction>()
         .HasKey(c => new { c.idAction, c.idFirefighter }); //doubleKey
        }

    }
}
