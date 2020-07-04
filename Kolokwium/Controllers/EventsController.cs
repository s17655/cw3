using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class EventsController : ControllerBase
        {
            private readonly IDbService _context;
            public EventsController(IDbService context)
            {
                _context = context;
            }

        [HttpGet("{id}")]
        public IActionResult GetArtistInfo()
            {
                return Ok();
            }

    }
}
