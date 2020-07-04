using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Models;
using test3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace test3.Controllers
{
    [Route("api/samples")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IDbService _context;
        public SampleController(IDbService context)
        {
            _context = context;
        }

        [HttpGet("error")]
        public IActionResult sampleMethod()
        {
            _context.sampleMethod();
            return Ok();
        }
    }
}
