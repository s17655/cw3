using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cw5.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public EnrollmentsController(IStudentsDbService service)
        {
            this._service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var enr = _service.EnrollStudent(request);
            if (enr != null)
            {
                return Created("Pomyślnie zapisano studenta", enr);
             }
            return BadRequest();
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var enr = _service.PromoteStudents(request);
            if (enr != null)
            {
                return Created("Pomyślnie promowano studentów", enr);
            }
            return NotFound();
        }
    }
}