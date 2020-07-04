using Test1.Exceptions;
using Test1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.DTO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Diagnostics.CodeAnalysis;

namespace Test1.Services
{
    public class ArtistsContextDbService : IDbService
    {
        private readonly ArtistsDbContext _context;
        public ArtistsContextDbService(ArtistsDbContext context)
        {
            _context = context;
        }

        public ArtistInfoRespond getArtistInfo(int id)
        {
            if (!(_context.Artists.Any(x => x.idArtist == id)))
            {
                return null;
            }

            string qnickname = _context.Artists.Where(x => x.idArtist == id).Select(x => x.nickname).FirstOrDefault(); 
            
            var qevents = (from zmienna1 in _context.Events
                         join zmienna2 in _context.ArtistEvents on zmienna1.idEvent equals zmienna2.idEvent
                         where zmienna2.idArtist==id
                         orderby zmienna2.performanceDate.Year descending
                         select new Event
                         {
                            idEvent=zmienna1.idEvent,
                            name=zmienna1.name,
                            startDate=zmienna1.startDate,
                            endDate=zmienna1.endDate
                         }).ToList();


            ArtistInfoRespond artResp = new ArtistInfoRespond
            {
                nickname = qnickname,
                events = qevents
            };
            return artResp;
        }

        public bool UpdatePerformance(PerformanceRequest request)
        {
            //ifexists
            //ifexists
            //ifbierzeudział
           if(!(_context.ArtistEvents.Any(x => x.idArtist == request.idArtist && x.idEvent == request.idEvent)))
            {
                //return new Exception("dfgh");
            }
            if (DateTime.Now > request.performanceDate)
            {
                //blad
            }
            ///....
            ///
            ArtistEvent ae = _context.ArtistEvents.Where(x => x.idArtist == request.idArtist && x.idEvent == request.idEvent)
                .FirstOrDefault();
                
            //..if null;
            return false;
        }


    }
}
