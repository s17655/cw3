using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Models
{
    public class Artist_Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idArtistEvent { get; set; }

        [ForeignKey("prescription")]
        public int idArtist { get; set; }
        [ForeignKey("event_")]
        public int idEvent { get; set; }
        public Artist artist { get; set; }
        public Event event_ { get; set; }

        public DateTime PerformanceDate { get; set; }
    }
}
