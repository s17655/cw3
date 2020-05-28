using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw7.Services
{
    public interface IStudentsDbService
    {
        EnrollmentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollmentResponse PromoteStudents(PromoteStudentsRequest request);
        bool ifStudentExists(string indexNumber);
        Student GetStudentByLoginPassword(string login, string pass);
        Student GetStudentByRefreshToken(string refreshToken);
        void SaveRefreshToken(string index, string token);

    }
}
