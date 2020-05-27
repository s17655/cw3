using Cw6.DTOs.Requests;
using Cw6.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw6.Services
{
    public interface IStudentsDbService
    {
        EnrollmentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollmentResponse PromoteStudents(PromoteStudentsRequest request);
        bool ifStudentExists(string indexNumber);
    }
}
