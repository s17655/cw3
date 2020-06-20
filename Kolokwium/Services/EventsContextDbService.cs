using Kolokwium.DTO;
using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
        public class EventsContextDbService : IDbService
        {
            private readonly EventsDbContext _context;
            public EventsContextDbService(EventsDbContext context)
            {
                _context = context;
            }

            public IEnumerable<ArtistReposnd> GetArtistInfo(int id)
            {
            var res = _context.Artists.Where(x => x.idArtist == id)
            .Join(_context.Artist_Events,
            artist => artist.idArtist,
            artist_event => artist_event.idArtist,
            (artist, artist_event) => new
            {
                artist.idArtist,
                artist.nickname,
                artist_event.PerformanceDate,
                artist_event.idEvent,
            }).Join(_context.Events,
            artist_event => artist_event.idEvent,
            event_ => event_.idEvent,
            (artist_event, event_) => new
            {
                artist_event.idArtist,
                artist_event.nickname,
                artist_event.PerformanceDate,
                event_.name,
                event_.startDate,
                event_.endDate
            }).OrderBy(y => y.startDate).ToList();


            return res;
            }

        }
}
