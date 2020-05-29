using Cw10_v2.DTOs.Requests;
using Cw10_v2.DTOs.Responses;
using Cw10_v2.Models;
using Cw10_v2.Models_old;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10_v2.Services
{
    public interface IStudentsDbService
    {
        EnrollmentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollmentResponse PromoteStudents(PromoteStudentsRequest request);
        bool ifStudentExists(string indexNumber);
        Student_old GetStudentByLoginPassword(string login, string pass);
        Student_old GetStudentByRefreshToken(string refreshToken);
        void SaveRefreshToken(string index, string token);
        List<Student> getStudents();
        Student GetStudentByIndex(string index);
        Student ModifyStudent(ModifyRequest request);
        public bool deleteStudent(string indexNumber);

    }
}
