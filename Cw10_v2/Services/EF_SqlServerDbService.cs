using Cw10_v2.DTOs.Requests;
using Cw10_v2.DTOs.Responses;
using Cw10_v2.Models;
using Cw10_v2.Models_old;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace Cw10_v2.Services
{

    public class EF_SqlServerDbService : IStudentsDbService
    {
        //private const string ConStr = "Data Source=db-mssql;Initial Catalog=s17655;Integrated Security=True";
        private const string ConStr = "Server=DESKTOP-4S302R6\\SQLEXPRESS;Database=APBD;Trusted_Connection=True";

        public EnrollmentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var enr = new EnrollmentResponse();
            // request
            APBDContext context = new APBDContext();

            //istnienie studiow
            Studies studies = context.Studies.Where(x => x.Name == request.Studies).FirstOrDefault();
            if (studies == null)
                return null;

            //wpis do tabeli enrollment
                //czy wpis istnieje
            Enrollment enrollment = context.Enrollment
                .Where(x => x.IdStudy == studies.IdStudy)
                .Where(x => x.Semester == 1)
                .FirstOrDefault();

            // jesli nie to dodaj wpis
            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    Semester = 1,
                    StartDate = DateTime.Today,
                    IdEnrollment = context.Enrollment.Select(x => x.IdEnrollment).Max()+1,
                    IdStudy = studies.IdStudy
                };
                context.Enrollment.Add(enrollment);
            }

            //sprawdzenie unikalnosci indeksu
            if (context.Student.Select(x => x.IndexNumber).Any(x => x == request.IndexNumber))
                return null;

            //dodanie studenta
            Student student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = DateTime.ParseExact(request.BirthDate, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture),
                IdEnrollment = enrollment.IdEnrollment
            };
            context.Student.Add(student);
            context.SaveChanges();

                // stworzenie Enrollmentu
     
            enr.Semester = enrollment.Semester;
            enr.StudyName = studies.Name;
            enr.StartDate = enrollment.StartDate.ToString();

            return enr;
        }

        public EnrollmentResponse PromoteStudents(PromoteStudentsRequest request)
        {
            APBDContext context = new APBDContext();
            //istnienie wpisów
            Enrollment enr = context.Enrollment
                .Where(x => x.Semester == request.Semester)
                .Where(x => x.IdStudy == context.Studies.Where(y => y.Name == request.Studies).FirstOrDefault().IdStudy)
                .FirstOrDefault();
            if (enr == null)
                return null;

            //istnienie wpisu +1
            Enrollment enr2 = context.Enrollment
                .Where(x => x.Semester == request.Semester+1)
                .Where(x => x.IdStudy == enr.IdStudy)
                .FirstOrDefault();

            if (enr2 == null)
                context.Enrollment.Add(enr2 = new Enrollment {
                    Semester = request.Semester+1,
                    StartDate = DateTime.Today,
                    IdEnrollment = context.Enrollment.Select(x => x.IdEnrollment).Max() + 1,
                    IdStudy = enr.IdStudy
                });


            //promocja studentów
            var students = context.Student
                .Where(x => x.IdEnrollment == enr.IdEnrollment).ToList();
            foreach(Student s in students){
                s.IdEnrollment = enr2.IdEnrollment;
            }
            context.SaveChanges();

                // stworzenie Enrollmentu
                EnrollmentResponse enrResp = new EnrollmentResponse();
                enrResp.Semester = enr2.Semester;
                enrResp.StudyName = request.Studies;
                enrResp.StartDate = enr2.StartDate.ToString();

                return enrResp;
            
        }

        public bool ifStudentExists(String indexNumber)
        {
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                // istnienie indeksu
                com.CommandText = "select IndexNumber from Student where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", indexNumber);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    sdr.Close();
                    return true;
                }
                sdr.Close();
                return false;
            }
        }
        public Student_old GetStudentByLoginPassword(string login, string pass)
        {
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                //get salt
                com.CommandText = "select Salt from Student " +
                    "where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", login);
                SqlDataReader sdr = com.ExecuteReader();
                if (!sdr.Read())
                {
                    return null;
                }
                string salt = sdr["Salt"].ToString();
                sdr.Close();

                string hashPass = GetHash(pass, salt);
                //Console.WriteLine(pass);
                //Console.WriteLine(salt);
                //Console.WriteLine(hashPass);

                com.CommandText = "select IndexNumber, FirstName, LastName, Role from Student " +
                    "where IndexNumber=@IndexNumber and Password=@Password";
                com.Parameters.AddWithValue("Password", hashPass);
                sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    Student_old student = new Student_old();
                    student.IndexNumber = sdr["IndexNumber"].ToString();
                    student.FirstName = sdr["FirstName"].ToString();
                    student.LastName = sdr["LastName"].ToString();
                    student.Role = sdr["Role"].ToString();
                    return student;
                }
                sdr.Close();
                return null;
            }
        }

        public Student_old GetStudentByRefreshToken(string refreshToken)
        {
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                com.CommandText = "select IndexNumber, FirstName, LastName, Role from Student " +
                    " where RefreshToken=@RefreshToken";
                com.Parameters.AddWithValue("RefreshToken", refreshToken);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    Student_old student = new Student_old();
                    student.IndexNumber = sdr["IndexNumber"].ToString();
                    student.FirstName = sdr["FirstName"].ToString();
                    student.LastName = sdr["LastName"].ToString();
                    student.Role = sdr["Role"].ToString();
                    return student;
                }
                sdr.Close();
                return null;
            }
        }


        public void SaveRefreshToken(string index, string token)
        {
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                com.CommandText = "update Student" +
                    " set RefreshToken=@RefreshToken " +
                    " where IndexNumber=@IndexNumber ";
                com.Parameters.AddWithValue("RefreshToken", token);
                com.Parameters.AddWithValue("IndexNumber", index);
                com.ExecuteNonQuery();
            }
        }

        private string GetHash(string password, string salt)
        {
            var ValueBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);
            return Convert.ToBase64String(ValueBytes);
        }


        public List<Student> getStudents()
        {
            var context = new APBDContext();
            return context.Student.ToList();
        }

        public bool deleteStudent(string indexNumber)
        {
            APBDContext context = new APBDContext();
            var student = GetStudentByIndex(indexNumber,context);
            if (student == null)
                return false;
            context.Student.Remove(student);
            context.SaveChanges();
            return true;
        }


        public Student ModifyStudent(ModifyRequest request)
        {
            APBDContext context = new APBDContext();
            var student = GetStudentByIndex(request.IndexNumber);
            if (student != null)
            {
                if (request.FirstName != null)
                    student.FirstName = request.FirstName;
                if (request.LastName != null)
                    student.LastName = request.LastName;
                if (request.BirthDate != null)
                    student.BirthDate = request.BirthDate;
                context.SaveChanges();
                return student;
            }
            return null;
        }

            public Student GetStudentByIndex(string index, APBDContext con)
        {
            APBDContext context;
            if (con == null)
            {
                context = new APBDContext();
            }
            else
            {
                context = con;
            }
            var student =  context.Student
                .Where(x=>x.IndexNumber==index)
                .FirstOrDefault();

            return student;
        }
        public Student GetStudentByIndex(string index)
        {
            var student = GetStudentByIndex(index, null);
            return student;
        }

    }
}
