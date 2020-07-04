using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Models;
using Test1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.DTO;

namespace Test1.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IDbService _context;
        public ArtistsController(IDbService context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getArtistInfo(int id)
        {
            id = 1;
            ArtistInfoRespond air = _context.getArtistInfo(id);
            if (air == null)
            {
                return NotFound("Artist not found");
            }
            return Ok(new { air.nickname,air.events });
        }

        [HttpPost("{idArtist}/events/{idEvent}")]
        public IActionResult updatePerfomanceDate(int idArtist,int idEvent, PerformanceRequest request)
        {

            return Ok();
        }


    }
}
