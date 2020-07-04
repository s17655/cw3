using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class ArtistEvent
    {
        [ForeignKey("artist")]
        public int idArtist { get; set; }
        [ForeignKey("event_")]
        public int idEvent { get; set; }
        public DateTime performanceDate { get; set; }
        public Artist artist { get; set; }
        public Event event_ { get; set; }

    }
}
