using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cw7.DTOs.Requests;
using Cw7.DTOs.Responses;
using Cw7.Models;
using Cw7.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Cw7.Controllers
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

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            //weryfikacja logina i hasłą z bazą
            Student student = _service.GetStudentByLoginPassword(request.Login, request.Haslo);
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
            Student student = _service.GetStudentByRefreshToken(request.refreshToken);
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

        private string getRefreshToken(Student student)
        {
            var refreshT = Guid.NewGuid();
            _service.SaveRefreshToken(student.IndexNumber, refreshT.ToString());
            return refreshT.ToString();
        }

            private string getActiveToken(Student student)
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