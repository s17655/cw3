using Cw11.DTO;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class DoctorsContextDbService: IDbService
    {
        private readonly DoctorsDbContext _context;
        public DoctorsContextDbService(DoctorsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }


        public bool DeleteDoctor(DeleteDoctorRequest request)
        {
            Doctor doc = _context.Doctors.Where(x => x.firstName == request.firstName)
                .Where(x => x.lastName == request.lastName).FirstOrDefault();
            if (doc == null)
                return false;
            _context.Doctors.Remove(doc);
            _context.SaveChanges();
            return true;
        }

        public Doctor modifyDoctor(DoctorRequest request)
        {
            var doc = _context.Doctors.Where(x => x.firstName == request.firstName)
                .Where(x => x.lastName == request.lastName).FirstOrDefault();
            if (doc == null)
                return null;
            doc.email = request.email;
            _context.SaveChanges();
            return doc;
        }

        public Doctor addDoctor(DoctorRequest request)
        {
            Doctor doc;
            _context.Doctors.Add(doc = new Doctor {
                firstName=request.firstName,
                lastName = request.lastName,
                email= request.email
            });
            _context.SaveChanges();
            return doc;
        }

        public void seed()
        {
            var con = _context;
            var doctor1 = new Doctor
            {
                firstName = "Jan",
                lastName = "Janiak",
                email = "jan@janiak.pl"
            };
            con.Doctors.Add(doctor1);
            var doctor2 = new Doctor
            {
                firstName = "Jerzy",
                lastName = "Jeż",
                email = "jerzy@jeż.pl"
            };
            con.Doctors.Add(doctor2);

            var patient1 = new Patient
            {
                firstName = "Lech",
                lastName = "Lach",
                birthDate = DateTime.ParseExact("2000-05-08", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)
            };
            con.Patients.Add(patient1);
            var patient2 = new Patient
            {
                firstName = "Andrzej",
                lastName = "Jedynak",
                birthDate = DateTime.ParseExact("2001-01-01", "yyyy-MM-dd",
                                                   System.Globalization.CultureInfo.InvariantCulture)
            };
            con.Patients.Add(patient2);

            var med1 = new Medicament
            {
                name = "Amol",
                description = "lek ziolowy",
                type = "plyn"
            };
            con.Medicaments.Add(med1);
            var med2 = new Medicament
            {
                name = "Ketanol",
                description = "lek przeciwbolowy",
                type = "tabletka"
            };
            con.Medicaments.Add(med2);

            var pre1 = new Prescription
            {
                date = DateTime.ParseExact("2011-01-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                dueDate = DateTime.ParseExact("2012-01-01", "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                patient = patient1,
                doctor = doctor1
            };
            con.Prescriptions.Add(pre1);
            var pre2 = new Prescription
            {
                date = DateTime.ParseExact("2013-01-01", "yyyy-MM-dd",
                           System.Globalization.CultureInfo.InvariantCulture),
                dueDate = DateTime.ParseExact("2014-01-01", "yyyy-MM-dd",
                           System.Globalization.CultureInfo.InvariantCulture),
                patient = patient2,
                doctor = doctor2
            };
            con.Prescriptions.Add(pre2);

            var preMed1 = new Prescription_Medicament
            {
                idMedicament = 1,
                prescription = pre1,
                medicament = med1,
                dose = 1,
                details = "detale"
            };
            con.Prescription_Medicaments.Add(preMed1);
            var preMed2 = new Prescription_Medicament
            {
                idMedicament = 2,
                prescription = pre2,
                medicament = med2,
                dose = 2,
                details = "detale2"
            };
            con.Prescription_Medicaments.Add(preMed2);
            con.SaveChanges();
        }

    }
}
