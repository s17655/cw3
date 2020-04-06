using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConStr = "Data Source=db-mssql;Initial Catalog=s17655;Integrated Security=True";
 
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConStr))
            using(SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = " select s.IndexNumber, s.FirstName, s.LastName, s.BirthDate, s2.Name, e.Semester" +
                                  " from Student s, Enrollment e, Studies s2"+
                                  " where s.IdEnrollment = e.IdEnrollment and s2.IdStudy = e.IdStudy; ";

                con.Open();
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    var st = new Student();
                    st.IdStudent = sdr["IndexNumber"].ToString();
                    st.FirstName = sdr["FirstName"].ToString();
                    st.LastName = sdr["LastName"].ToString();
                    st.BirthDate = sdr["BirthDate"].ToString();
                    st.StudiesName = sdr["Name"].ToString();
                    st.Semester = (int)sdr["Semester"];
                    list.Add(st);
                }
            }

            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            var list = new List<Enrollment>();

            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = " select s.IndexNumber, e.Semester, e.StartDate, s2.Name " +
                    "from Enrollment e, Student s, Studies s2 " +
                    "where e.IdEnrollment=s.IdEnrollment and s2.IdStudy = e.IdStudy and s.IndexNumber=@index";
                com.Parameters.AddWithValue("index", id);

                con.Open();
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    var enr = new Enrollment();
                    enr.IdStudent = sdr["IndexNumber"].ToString();
                    enr.Semester = (int)sdr["Semester"];
                    enr.StartDate = sdr["StartDate"].ToString();
                    enr.StudyName = sdr["Name"].ToString();
                    list.Add(enr);
                }
            }

            if (!list.Any())
            {
                return NotFound("Nie znaleziono wpisów dla danego studenta");
            }
            return Ok(list);
        }

        /* zad 4.3. i 4.4
         
        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            var list = new List<Enrollment>();

            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = " select s.IndexNumber, e.Semester, e.StartDate, s2.Name " +
                    "from Enrollment e, Student s, Studies s2 " +
                    "where e.IdEnrollment=s.IdEnrollment and s2.IdStudy = e.IdStudy and s.IndexNumber=" + id;
                //aby usunąć tabelę Students:
                //id = "x';DROP TABLE Students; --";


                con.Open();
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    var enr = new Enrollment();
                    enr.IdStudent = sdr["IndexNumber"].ToString();
                    enr.Semester = (int)sdr["Semester"];
                    enr.StartDate = sdr["StartDate"].ToString();
                    enr.StudyName = sdr["Name"].ToString();
                    list.Add(enr);
                }
            }

            if (!list.Any())
            {
                return NotFound("Nie znaleziono wpisów dla danego studenta");
            }
            return Ok(list);
        }
        */
    }
}