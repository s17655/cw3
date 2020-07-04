using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium2.DTO;
using Kolokwium2.Models;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers
{
    [Route("api")]
    [ApiController]
    public class FireController : ControllerBase
    {
            private readonly IDbService _context;
            public FireController(IDbService context)
            {
                _context = context;
            }

            [HttpGet("firefighters/{id}/actions")]
            public IActionResult getActions(int id)
            {
                ICollection<ActionResponse> coll = _context.getActions(id);
            if (coll == null)
            {
                return BadRequest("W bazie nie istnieje strazak o podanym id");
            }

                return Ok(coll);
            }

            [HttpPost("actions/{id}/fire-trucks")]
            public IActionResult signFireTruckForAction(int id, FireTruckSignRequest request)
            {
            if (request.idAction != id)
            {
                return BadRequest("Zadanie nie jest spojne ze sciezka wywolania");
            }

                bool signed = _context.signFireTruckForAction(request);
                return Ok("Pomyslnie przypisano woz do akcji");
            }

 
        }
}
