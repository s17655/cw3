using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class DoctorsDbContext: DbContext
    {
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DoctorsDbContext()
        {
        }

        public DoctorsDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
