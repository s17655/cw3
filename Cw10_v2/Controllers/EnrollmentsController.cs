using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cw10_v2.DTOs.Requests;
using Cw10_v2.DTOs.Responses;
using Cw10_v2.Models_old;
using Cw10_v2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Cw10_v2.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public IConfiguration Configuration { get; set; }
        public EnrollmentsController(IStudentsDbService service, IConfiguration configuration)
        {
            this._service = service;
            Configuration = configuration;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "employee")]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "employee")]
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