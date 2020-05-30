using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw11.DTO;
using Cw11.Models;
using Cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [Route("api/doctors")]
    [ApiController]


    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _context;
        public DoctorsController(IDbService context)
        {
            _context = context;
        }

        [HttpPost("seed")]
        public IActionResult seed()
        {
            _context.seed();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.GetDoctors());
        }

        [HttpDelete]
        public IActionResult DeleteDoctor(DeleteDoctorRequest request)
        {
            bool res = _context.DeleteDoctor(request);
            if (!res)
                return BadRequest("Brak podanego doktora");
            return Ok();
        }

        [HttpPut]
        public IActionResult ModifyDoctor(DoctorRequest request)
        {
            Doctor doc = _context.modifyDoctor(request);
            if (doc == null)
                return BadRequest("Brak podanego doktora");
            return Ok("Zmodyfikowano doktora");
        }

        [HttpPost]
        public IActionResult AddDoctor(DoctorRequest request)
        {
            Doctor doc = _context.addDoctor(request);
            return Created("Utworzono doktora",doc);
        }

    }
}
