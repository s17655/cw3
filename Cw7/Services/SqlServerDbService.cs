using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace Cw7.Services
{

    public class SqlServerDbService : IStudentsDbService
    {
        //private const string ConStr = "Data Source=db-mssql;Initial Catalog=s17655;Integrated Security=True";
        private const string ConStr = "Server=DESKTOP-4S302R6\\SQLEXPRESS;Database=APBD;Trusted_Connection=True";

        public EnrollmentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var enr = new EnrollmentResponse();
            // request
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;

                // istnienie studiów
                com.CommandText = "select idStudy from Studies where Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);
                SqlDataReader sdr = com.ExecuteReader();
                if (!sdr.Read())
                {
                    sdr.Close();
                    tran.Rollback();
                    return null;
                }
                int idStudy = (int)sdr["IdStudy"];
                sdr.Close();

                // wpis w tabeli enrollment
                int idEnrollment;
                com.CommandText = "select IdEnrollment from Enrollment" +
                    " where idStudy=@idStudy and Semester=1" +
                    " order by IdEnrollment";
                com.Parameters.AddWithValue("idStudy", idStudy);
                sdr = com.ExecuteReader();
                if (!sdr.Read())
                {
                    sdr.Close();

                    //pobranie max idEnrollment
                    int idMaxEnroll = 0;
                    com.CommandText = "select max(idEnrollment) as idEnrollment from Enrollment";
                    sdr = com.ExecuteReader();
                    if (sdr.Read())
                    {
                        idMaxEnroll = (int)sdr["idEnrollment"];
                    }
                    sdr.Close();
                    idEnrollment = idMaxEnroll + 1;

                    //dodanie wpisu
                    com.CommandText = "insert into Enrollment(Semester, IdStudy, StartDate, idEnrollment) " +
                        " values(@Semester,@IdStudy,@StartDate, @idEnroll);";
                    com.Parameters.AddWithValue("Semester", 1);
                    com.Parameters.AddWithValue("StartDate", DateTime.Today);
                    com.Parameters.AddWithValue("idEnroll", idEnrollment);
                    com.ExecuteNonQuery();
                }
                else
                {
                    idEnrollment = (int)sdr["IdEnrollment"];
                    sdr.Close();
                }

                //sprawdzenie unikalnosci indeksu
                com.CommandText = "select LastName from Student" +
                    " where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    sdr.Close();
                    tran.Rollback();
                    return null;
                }
                sdr.Close();

                //dodanie studenta
                com.CommandText = "insert into Student" +
                    "(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " +
                    "values(@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment) ";
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.LastName);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                com.ExecuteNonQuery();

                tran.Commit();

                // stworzenie Enrollmentu
                com.CommandText = "select Semester, Name, StartDate from Studies, Enrollment " +
                     "where Studies.IdStudy=Enrollment.IdStudy " +
                     "and idEnrollment=@IdEnrollment ";

                sdr = com.ExecuteReader();
                sdr.Read();
                enr.Semester = (int)(sdr["Semester"]);
                enr.StudyName = sdr["Name"].ToString();
                enr.StartDate = sdr["StartDate"].ToString();

            }
            return enr;
        }

        public EnrollmentResponse PromoteStudents(PromoteStudentsRequest request)
        {
            using (var con = new SqlConnection(ConStr))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                // istnienie wpisów
                com.CommandText = "select idEnrollment from Enrollment e, Studies s " +
                    " where e.idStudy=s.idStudy " +
                    " and s.Name=@Studies and e.Semester=@Semester";
                com.Parameters.AddWithValue("Studies", request.Studies);
                com.Parameters.AddWithValue("Semester", request.Semester);
                SqlDataReader sdr = com.ExecuteReader();
                if (!sdr.Read())
                {
                    sdr.Close();
                    return null;
                }
                sdr.Close();

                // procedura skladowana
                SqlCommand cmd = new SqlCommand("dbo.promoteStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Studies", SqlDbType.VarChar).Value = request.Studies;
                cmd.Parameters.Add("@Semester", SqlDbType.Int).Value = request.Semester;
                //cmd.ExecuteNonQuery();

                // stworzenie Enrollmentu
                EnrollmentResponse enr = new EnrollmentResponse();
                sdr = cmd.ExecuteReader();
                sdr.Read();
                enr.Semester = (int)(sdr["Semester"]);
                enr.StudyName = sdr["Name"].ToString();
                enr.StartDate = sdr["StartDate"].ToString();

                return enr;
            }
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
        public Student GetStudentByLoginPassword(string login, string pass)
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
                    Student student = new Student();
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

        public Student GetStudentByRefreshToken(string refreshToken)
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
                    Student student = new Student();
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

    }
}
