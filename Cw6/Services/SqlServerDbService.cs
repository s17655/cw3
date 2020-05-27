using Cw6.DTOs.Requests;
using Cw6.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace Cw6.Services
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
    }
}
