using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw5.Services
{
    public interface IStudentsDbService
    {
        EnrollmentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollmentResponse PromoteStudents(PromoteStudentsRequest request);
    }
}
