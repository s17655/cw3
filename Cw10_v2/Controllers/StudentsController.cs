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
using Cw10_v2.Models;

namespace Cw10_v2.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsDbService _service;
        public IConfiguration Configuration { get; set; }
        public StudentsController(IStudentsDbService service, IConfiguration configuration)
        {
            this._service = service;
            Configuration = configuration;
        }
        
        [HttpGet]
        public IActionResult getStudents()
        {
            return Ok(_service.getStudents());
        }

        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(string indexNumber)
        {
            var success = _service.deleteStudent(indexNumber);
            if(success)
                return Ok("Usunieto studenta");
            return BadRequest("Nie znaleziono studenta");
        }

        [HttpPut]
        public IActionResult ModifyStudent(ModifyRequest request)
        {
            var student = _service.ModifyStudent(request);   
            if (student!=null)
                return Ok(student);
            return BadRequest("Nie znaleziono studenta");
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            //weryfikacja logina i hasłą z bazą
            Student_old student = _service.GetStudentByLoginPassword(request.Login, request.Haslo);
            if (student == null)
            {
                return Unauthorized("Logowanie zakonczone niepowodzeniem");
            }

            string activeToken = getActiveToken(student);
            string refreshT = getRefreshToken(student);

            return Ok(new
            {
                token = activeToken,
                refreshToken = refreshT
            });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh(RefreshRequest request)
        {
            //weryfikacja tokena
            Student_old student = _service.GetStudentByRefreshToken(request.refreshToken);
            if (student == null)
            {
                return Unauthorized("Odswiezenie zakonczylo sie niepowodzeniem");
            }

            string activeToken = getActiveToken(student);
            string refreshT = getRefreshToken(student);

            return Ok(new
            {
                token = activeToken,
                refreshToken = refreshT
            });
        }

        private string getRefreshToken(Student_old student)
        {
            var refreshT = Guid.NewGuid();
            _service.SaveRefreshToken(student.IndexNumber, refreshT.ToString());
            return refreshT.ToString();
        }

        private string getActiveToken(Student_old student)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, student.IndexNumber),
                new Claim(ClaimTypes.Name, student.LastName),
                new Claim(ClaimTypes.Role, student.Role),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}